using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZNParser.Reader
{
    public class BZNStreamReader : IDisposable
    {
        /// <summary>
        /// Lookup for complex variables in ASCII BZN files, the value is the number of sub-tokens that make up the complex variable.
        /// </summary>
        private static readonly Dictionary<string, int> ComplexStringTokenSizeMap = new Dictionary<string, int>
        {
            {"points", 2},
            {"pos", 3},
            {"v", 3},
            {"omega", 3},
            {"Accel", 3},
            {"euler", 9},
            {"dropMat", 12},
            {"transform", 12},
            {"startMat", 12},
            {"saveMatrix", 12},
            {"buildMatrix", 12},
        };

        public Stream BaseStream { get; private set; } // Underlying Stream
        private bool inBinary; // Are we currently reading binary tokens?

        /// <summary>
        /// BZN file started in binary.
        /// Only used for Battlezone N64 files.
        /// </summary>
        public bool StartBinary { get; private set; }
        /// <summary>
        /// BZN file has binary fields.
        /// Normal BZNs always start in ASCII mode and switch to binary mode later.
        /// </summary>
        public bool HasBinary { get; private set; }
        /// <summary>
        /// BZN file is big endian.
        /// This applies to Battlezone N64 files.
        /// </summary>
        public bool IsBigEndian { get; private set; }
        /// <summary>
        /// Size of the type field in the binary tokens.
        /// In Battlezone N64 this is 2.
        /// In Battlezone this is 2.
        /// In Battlezone II this is 2.
        /// In Star Trek Armada this is 4.
        /// In Star Trek Armada 2 this is 4.
        /// </summary>
        public byte TypeSize { get; private set; }
        /// <summary>
        /// Size of the size field in the binary tokens.
        /// In Battlezone N64 this is 0 (not present).
        /// In Battlezone this is 2 (The type enumeration only needs one byte).
        /// In Battlezone II this is 1.
        /// In Star Trek Armada this is 4 (garbage possible in all but lowest sig byte).
        /// In Star Trek Armada 2 this is 4 (garbage possible in all but lowest sig byte).
        /// </summary>
        public byte SizeSize { get; private set; }
        /// <summary>
        /// Version of the BZN file.
        /// This can be inconsistent between different BZN types but has rare utility the tokenizer level such as maximum ASCII line length control.
        /// </summary>
        public int Version { get; private set; }
        /// <summary>
        /// Is this a save or a map? Not very useful for the tokenizer and might be removed later.
        /// </summary>
        public int SaveType { get; private set; }
        /// <summary>
        /// Used by Battlezone N64 BZNs which must have even sized data.
        /// This might actually need to be 16bit alignment so do figure that out.
        /// </summary>
        public bool ValuesArePadded { get; private set; }
        /// <summary>
        /// Game specific variant of the BZN format.
        /// </summary>
        public BZNFormat Format { get; set; }

        /// <summary>
        /// Pre-read tokens used to analyze the file.
        /// Store with their start and end offsets to fake-seek the stream.
        /// </summary>
        private List<(long Position, IBZNToken Token, long TailPosition)> TokenBuffer = new List<(long, IBZNToken, long TailPosition)>();
        /// <summary>
        /// End offset of header fields.
        /// </summary>
        private long EndOfHeaderOffset = 0;

        public BZNStreamReader(Stream stream)
        {
            long startPosition = stream.Position;

            Format = BZNFormat.Battlezone;

            this.BaseStream = stream;

            long position = stream.Position;
            BinaryReader reader = new BinaryReader(stream);
            //using (BinaryReader reader = new BinaryReader(stream))
            {
                // assume BZNs always start with a version, check for the string format
                char[] versionname = reader.ReadChars(13);
                //HasBinary = StartBinary = inBinary = !versionname.SequenceEqual(@"version [1] =".ToCharArray());
                //HasBinary = StartBinary = inBinary = !versionname.SequenceEqual(@"version [1] =".ToCharArray()) && !versionname.SequenceEqual(@"aversion [1] ".ToCharArray());
                HasBinary = StartBinary = inBinary = !versionname.All(c => !char.IsControl(c));

                stream.Position = position;

                bool TypeSizeSet = false;
                //TypeSize = 0; // BZn64
                //TypeSize = 1; // BZ2
                TypeSize = 2; // BZ1
                SizeSize = 2; // BZ

                // we are starting in binary, so check for BigEndian since this could be an n64 file
                if (inBinary)
                {
                    byte[] First2Bytes = new byte[2];
                    reader.Read(First2Bytes, 0, 2);
                    if (First2Bytes[0] == 0x00 && First2Bytes[1] != 0x00)
                    {
                        IsBigEndian = true;
                        TypeSize = 0; // BZn64
                        ValuesArePadded = true;
                        TypeSizeSet = true;
                        Format = BZNFormat.BattlezoneN64;
                    }
                    stream.Position = position;
                }

                long tmpPosition = stream.Position;
                IBZNToken VersionToken = ReadToken();
                Version = VersionToken.GetInt32();
                TokenBuffer.Add((tmpPosition, VersionToken, stream.Position));

                tmpPosition = position = stream.Position;
                IBZNToken SaveTypeToken = ReadToken();
                if (!inBinary && SaveTypeToken.Validate("saveType"))
                {
                    SaveType = SaveTypeToken.GetInt32();
                    TypeSize = 1; // BZ2 has saveType flag, BZ1 does not
                    TypeSizeSet = true;
                    TokenBuffer.Add((tmpPosition, SaveTypeToken, stream.Position));
                    Format = BZNFormat.Battlezone2;
                }
                else if (!inBinary && SaveTypeToken.Validate("saveGameDesc"))
                {
                    TypeSize = 4; // Star Trek Armada, 3 bytes are garbage
                    TypeSizeSet = true;
                    SizeSize = 4;
                    TokenBuffer.Add((tmpPosition, SaveTypeToken, stream.Position));
                    Format = BZNFormat.StarTrekArmada;
                }
                else
                {
                    // we didn't read a saveType, walk back
                    stream.Position = position;
                }

                if (Version > 1022)
                {
                    tmpPosition = stream.Position;
                    IBZNToken BinaryToken = ReadToken();
                    if (BinaryToken.Validate("binarySave"))
                    {
                        HasBinary = inBinary = BinaryToken.GetBoolean();
                        TokenBuffer.Add((tmpPosition, BinaryToken, stream.Position));
                    }
                    else if (BinaryToken.Validate("BinaryMode"))
                    {
                        HasBinary = inBinary = BinaryToken.GetBoolean();
                        TypeSize = 4; // Star Trek Armada, 3 bytes are garbage
                        TypeSizeSet = true;
                        SizeSize = 4;
                        TokenBuffer.Add((tmpPosition, BinaryToken, stream.Position));
                        Format = BZNFormat.StarTrekArmada2;
                    }
                    else
                    {
                        stream.Position = position;
                    }
                }

                // We're the default type size, so let's inspect our size
                if (!TypeSizeSet)
                {
                    // interrogate, might need this to repeat earlier if the file starts binary but unsure
                }

                EndOfHeaderOffset = stream.Position;

                // check for special case BZ2's bz2001.bzn
                // we might be the "bz2001.bzn" file from BZ2 that is not in the BZ1 patch continuum but we register as a BZ1 type BZN
                if (Format == BZNFormat.Battlezone && !HasBinary)
                {
                    IBZNToken tok = ReadToken();
                    if (tok.Validate("msn_filename"))
                    {
                        tok = ReadToken();
                        if (tok.Validate("seq_count", BinaryFieldType.DATA_LONG))
                        {
                            tok = ReadToken();
                            if (tok.Validate("saveType", BinaryFieldType.DATA_LONG))
                            {
                                SaveType = tok.GetInt32();
                                Format = BZNFormat.Battlezone2;
                            }
                        }
                    }
                }

                stream.Position = startPosition;
            }
        }

        public void Dispose()
        {
            if (BaseStream != null) BaseStream.Close();
        }

        /// <summary>
        /// Read the next token from the stream.
        /// </summary>
        /// <returns></returns>
        public IBZNToken ReadToken()
        {
            if (BaseStream.Position < EndOfHeaderOffset)
            {
                // return buffered tokens we used to analyze the file
                foreach (var pair in TokenBuffer)
                {
                    if (pair.Position == BaseStream.Position)
                    {
                        BaseStream.Position = pair.TailPosition;
                        return pair.Token;
                    }
                }
            }

            if (!inBinary)
            {
                return ReadStringToken(BaseStream);
            }
            else
            {
                return ReadBinaryToken(BaseStream);
            }
        }

        /// <summary>
        /// Read a string value or string validation token from the file stream.
        /// </summary>
        /// <param name="filestream"></param>
        /// <returns></returns>
        private IBZNToken ReadStringToken(Stream filestream)
        {
            if (filestream.Position >= filestream.Length) return null;

            string rawLine = ReadStringLine(filestream);

            if (rawLine.StartsWith("[") && rawLine.EndsWith("]"))
            {
                return new BZNTokenValidation(rawLine.Substring(1, rawLine.Length - 2));
            }

            return ReadStringValueToken(filestream, rawLine);
        }

        /// <summary>
        /// Read a string value from the file stream.
        /// </summary>
        /// <param name="filestream"></param>
        /// <param name="rawLine"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IBZNToken ReadStringValueToken(Stream filestream, string rawLine)
        {
            long pos = filestream.Position;
            string[] line = rawLine.Split(' ', 4);

            if (line[1] == "=")
            {
                line = rawLine.Split(' ', 3);
                string name = line[0];

                if (ComplexStringTokenSizeMap.ContainsKey(name))
                {
                    int count = 1;

                    IBZNToken[][] values = new IBZNToken[count][];
                    for (int subSectionCounter = 0; subSectionCounter < count; subSectionCounter++)
                    {
                        values[subSectionCounter] = new IBZNToken[ComplexStringTokenSizeMap[name]];
                        for (int constructCounter = 0; constructCounter < ComplexStringTokenSizeMap[name]; constructCounter++)
                        {
                            string rawLineInner = ReadStringLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueToken(filestream, rawLineInner);
                        }
                    }

                    return new BZNTokenNestedString(name, values);
                }
                else
                {
                    if (line.Length == 2)
                        //return new BZNTokenString(name, null);
                        return new BZNTokenString(name, new string[] { string.Empty });

                    string value = line[2];

                    return new BZNTokenString(name, new string[] { value });
                }
            }
            else if (line[2] == "=")
            {
                string name = line[0];
                int count = int.Parse(line[1].Substring(1, line[1].Length - 2));

                if (count == 0) return new BZNTokenString(name, new string[0]);

                if (ComplexStringTokenSizeMap.ContainsKey(name))
                {
                    IBZNToken[][] values = new IBZNToken[count][];
                    for (int subSectionCounter = 0; subSectionCounter < count; subSectionCounter++)
                    {
                        values[subSectionCounter] = new IBZNToken[ComplexStringTokenSizeMap[name]];
                        for (int constructCounter = 0; constructCounter < ComplexStringTokenSizeMap[name]; constructCounter++)
                        {
                            string rawLineInner = ReadStringLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueToken(filestream, rawLineInner);
                        }
                    }

                    return new BZNTokenNestedString(name, values);
                }
                else
                {
                    string[] values = new string[count];

                    for (int lineNum = 0; lineNum < count; lineNum++)
                    {
                        long new_pos = filestream.Position;
                        string new_rawLine = ReadStringLine(filestream).TrimEnd('\r', '\n');
                        string[] new_line = new_rawLine.TrimStart().Split(' ', 4);

                        if ((new_line.Length > 1 && new_line[1] == "=") || (new_line.Length > 2 && new_line[2] == "="))
                        {
                            // special case where we didn't have a value for some reason (buggy printed VEC2Ds

                            // points [1] =
                            //   x [1] =
                            // 32
                            //   z [1] =
                            // pathType = 00000000
                            // ...
                            // points [1] =
                            //   x [1] =
                            //   z [1] =
                            // -32
                            // pathType = 00000000

                            values[lineNum] = string.Empty;
                            filestream.Position = new_pos;
                        }
                        else
                        {
                            values[lineNum] = new_rawLine;
                        }
                    }

                    return new BZNTokenString(name, values);
                }
            }
            else
            {
                throw new Exception("Error reading ASCII data, \"=\" not found where expected.");
            }
        }

        /// <summary>
        /// Read a binary token from the file stream.
        /// </summary>
        /// <param name="filestream"></param>
        /// <returns></returns>
        private IBZNToken ReadBinaryToken(Stream filestream)
        {
            if (filestream.Position >= filestream.Length) return null;
            
            byte[] number = new byte[4];
            uint type = 0;
            if (TypeSize > 0)
            {
                if (IsBigEndian)
                {
                    filestream.Read(number, sizeof(uint) - TypeSize, TypeSize);
                    type = BitConverter.ToUInt32(number.Reverse().ToArray(), 0); // for bz1 this is only 1 byte, n64 lacks type
                }
                else
                {
                    filestream.Read(number, 0, TypeSize);
                    type = BitConverter.ToUInt32(number, 0); // for bz1 this is only 1 byte, n64 lacks type
                }
                type &= 0xff;
            }
            else
            {
                type = (uint)BinaryFieldType.DATA_UNKNOWN;
            }
            uint Size = 0;
            if (IsBigEndian)
            {
                filestream.Read(number, sizeof(uint) - SizeSize, SizeSize);
                Size = BitConverter.ToUInt32(number.Reverse().ToArray(), 0);
            }
            else
            {
                filestream.Read(number, 0, SizeSize);
                Size = BitConverter.ToUInt32(number, 0);
            }

            byte[] data = new byte[Size];
            filestream.Read(data, 0, (int)Size);

            if (ValuesArePadded)
            {
                if (Size % 2 != 0) filestream.ReadByte(); // deal with padding
            }

            return new BZNTokenBinary((BinaryFieldType)type, data, IsBigEndian);
        }

        /// <summary>
        /// Read a string from the file stream.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private string ReadStringLine(Stream fileStream)
        {
            string buffer = string.Empty;

            for (int idx = 0; fileStream.Position < fileStream.Length; idx++)
            {
                byte character = (byte)fileStream.ReadByte();

                if (character == 0x0D)
                {
                    fileStream.ReadByte(); // 0x0A

                    // Version 1180 line width 4095
                    // Version 1192 line width uncapped?
                    if (Version <= 1180 && idx == 4095)
                    {
                        idx = -1;

                        continue;
                    }

                    break;
                }
                else
                {
                    buffer += (char)character;
                }
            }

            return buffer;
        }
    }
}
