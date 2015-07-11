using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools
{
    public class BZNReader : IDisposable
    {
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
        };

        private bool n64Data;
        private bool inBinary;
        private int _Version;
        private Stream datastream;

        public Stream stream { get { return datastream; } }
        public int Version { get { return _Version; } }
        public bool BinaryMode { get { return inBinary; } }
        public bool N64 { get { return n64Data; } }

        public BZNReader(Stream filestream, bool inBinary = false, bool n64Data = false)
        {
            this.n64Data = n64Data;
            this.inBinary = inBinary;
            this.datastream = filestream;

            //Console.WriteLine("Start Mode: {0}", this.inBinary ? "BIN" : "ASCII");
            //Console.WriteLine("Byte Order and Padding: {0}", this.n64Data ? "N64" : "PC");
            //Console.WriteLine();

            IBZNToken VersionToken = ReadToken();
            this._Version = VersionToken.GetInt32();

            if (n64Data || Version > 1022)
            {
                IBZNToken BinaryToken = ReadToken();
                this.inBinary = BinaryToken.GetBoolean();
            }
            else
            {
                this.inBinary = false;
            }

            //Console.WriteLine();
            //Console.WriteLine("Version: {0}", this.Version);
            //Console.WriteLine("Current Mode: {0}", this.inBinary ? "BIN" : "ASCII");
            //Console.WriteLine();
        }

        public IBZNToken ReadToken()
        {
            if (!inBinary)
            {
                return ReadStringValue(datastream);
            }
            else
            {
                return ReadBinaryValue(datastream);
            }
            //return null;
        }

        private IBZNToken ReadStringValue(Stream filestream)
        {
            if (filestream.Position >= filestream.Length) return null;

            string rawLine = ReadLine(filestream);

            if (rawLine.StartsWith("[") && rawLine.EndsWith("]"))
            {
                return new BZNValidationToken(rawLine.Substring(1, rawLine.Length - 2));
            }

            return ReadStringValueSub(filestream, rawLine);
        }

        private IBZNToken ReadStringValueSub(Stream filestream, string rawLine)
        {
            string[] line = rawLine.Split(' ');

            if (line[1] == "=")
            {
                string name = line[0];
                //bool isArray = false;

                if (ComplexStringTokenSizeMap.ContainsKey(name))
                {
                    int count = 1;

                    IBZNToken[][] values = new IBZNToken[count][];
                    for (int subSectionCounter = 0; subSectionCounter < count; subSectionCounter++)
                    {
                        values[subSectionCounter] = new IBZNToken[ComplexStringTokenSizeMap[name]];
                        for (int constructCounter = 0; constructCounter < ComplexStringTokenSizeMap[name]; constructCounter++)
                        {
                            string rawLineInner = ReadLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueSub(filestream, rawLineInner);
                        }
                    }

                    return new BZNTokenNestedString(name, values);
                }
                else
                {
                    string value = line[2];

                    //Console.WriteLine("ASCII\t\"{0}\"\tValidation: {1}", value, name);

                    return new BZNTokenString(name, new string[] { value });
                }
            }
            else if (line[2] == "=")
            {
                string name = line[0];
                //bool isArray = true;
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
                            string rawLineInner = ReadLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueSub(filestream, rawLineInner);
                        }
                    }

                    return new BZNTokenNestedString(name, values);
                }
                else
                {
                    string[] values = new string[count];

                    for (int lineNum = 0; lineNum < count; lineNum++)
                    {
                        values[lineNum] = ReadLine(filestream).TrimEnd('\r', '\n');
                    }

                    //Console.WriteLine("ASCII\t[0] \"{0}\"\tValidation: {1}", values[0], name);
                    //for (int x = 0; x < values.Length; x++)
                    //{
                    //    Console.WriteLine("ASCII\t[{1}] \"{0}\"", values[x], x);
                    //}

                    return new BZNTokenString(name, values);
                }
            }
            else
            {
                throw new Exception("Error reading ASCII data, \"=\" not found where expected.");
            }
        }

        private IBZNToken ReadBinaryValue(Stream filestream)
        {
            if (filestream.Position >= filestream.Length) return null;

            byte[] number = new byte[2];
            ushort type = 0;
            if (!n64Data)
            {
                filestream.Read(number, 0, 2); type = BitConverter.ToUInt16(number, 0); // for bz1 this is only 1 byte, n64 lacks type
            }
            ushort Size = 0;
            filestream.Read(number, 0, 2);
            if (n64Data)
            {
                Size = BitConverter.ToUInt16(number.Reverse().ToArray(), 0);
            }
            else
            {
                Size = BitConverter.ToUInt16(number, 0);
            }

            byte[] data = new byte[Size];
            filestream.Read(data, 0, Size);

            if (n64Data)
            {
                if (Size % 2 != 0) filestream.ReadByte(); // deal with padding
            }

            string stringVal = BitConverter.ToString(data).Replace("-", string.Empty);
            if (stringVal.Length > 32) stringVal = stringVal.Substring(0, 32-3) + @"...";
            //Console.WriteLine("BIN\t{0}\t{1}\tValidation: {2}", Size, stringVal, Enum.GetName(typeof(FIELD_TYPEz), type));

            return new BZNTokenBinary((BinaryFieldType)type, data, n64Data);
        }

        public void Dispose()
        {
            if (stream != null) stream.Close();
        }

        private string ReadLine(Stream fileStream)
        {
            byte[] lineBuffer = new byte[64];

            int idx = 0;

            for (; ; )
            {
                byte character = (byte)fileStream.ReadByte();

                if(character == 0x0D)
                {
                    fileStream.ReadByte(); // 0x0A
                    break;
                }

                lineBuffer[idx++] = character;
            }

            return Encoding.ASCII.GetString(lineBuffer, 0, idx);
        }
    }
}
