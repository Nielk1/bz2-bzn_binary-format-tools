using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BinaryBZNFile
{
    public enum BinaryFieldType : byte
    {
        DATA_VOID = 0, //0x00
        DATA_BOOL = 1, //0x01
        DATA_CHAR = 2, //0x02
        DATA_SHORT = 3, //0x03
        DATA_LONG = 4, //0x04
        DATA_FLOAT = 5, //0x05
        DATA_DOUBLE = 6, //0x06
        DATA_ID = 7, //0x07
        DATA_PTR = 8, //0x08
        DATA_VEC3D = 9, //0x09
        DATA_VEC2D = 10,//0x0A
        DATA_MAT3DOLD = 11,//0x0B
        DATA_MAT3D = 12,//0x0C
        DATA_STRING = 13,//0x0D
        DATA_QUAT = 14, //0x0E

        DATA_UNKNOWN = 255
    }

    public interface IBZNToken
    {
        bool GetBoolean(int index = 0);
        Int32 GetInt32(int index = 0);
        UInt32 GetUInt32(int index = 0);
        UInt32 GetUInt32H(int index = 0);
        Int16 GetInt16(int index = 0);
        UInt16 GetUInt16(int index = 0);
        string GetString(int index = 0);
        float GetSingle(int index = 0);
        Vector3D GetVector3D(int index = 0);
        Vector2D GetVector2D(int index = 0);
        Matrix GetMatrix(int index = 0);
        Euler GetEuler(int index = 0);

        bool IsValidationOnly();
        bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN);
    }

    public interface IBZNStringToken : IBZNToken
    {

    }

    public class BZNValidationToken : IBZNToken
    {
        private string name;

        public BZNValidationToken(string name)
        {
            this.name = name;
        }

        public bool GetBoolean(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Int32 GetInt32(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt32 GetUInt32(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt32 GetUInt32H(int index = 0) { return GetUInt32(); }
        public Int16 GetInt16(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt16 GetUInt16(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public float GetSingle(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public string GetString(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Vector3D GetVector3D(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Vector2D GetVector2D(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Matrix GetMatrix(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Euler GetEuler(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }

        public bool IsValidationOnly() { return true; }

        public override string ToString()
        {
            return "VALDT\t[" + name + "]";
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            return this.name == name;
        }
    }

    public class BZNBinaryToken : IBZNToken
    {
        private BinaryFieldType type;
        private byte[] data;
        private bool n64Data;

        public BZNBinaryToken(BinaryFieldType fIELD_TYPE, byte[] data, bool n64Data)
        {
            // TODO: Complete member initialization
            this.type = fIELD_TYPE;
            this.data = data;
            this.n64Data = n64Data;
        }

        public bool GetBoolean(int index = 0)
        {
            if (index >= data.Length / sizeof(bool)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToBoolean(data.Skip(index * sizeof(bool)).Take(sizeof(bool)).Reverse().ToArray(), 0);
            return BitConverter.ToBoolean(data, index * sizeof(bool));
        }

        public Int32 GetInt32(int index = 0)
        {
            if (index >= data.Length / sizeof(Int32)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToInt32(data.Skip(index * sizeof(Int32)).Take(sizeof(Int32)).Reverse().ToArray(), 0);
            return BitConverter.ToInt32(data, index * sizeof(Int32));
        }

        public UInt32 GetUInt32(int index = 0)
        {
            if (index >= data.Length / sizeof(UInt32)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToUInt32(data.Skip(index * sizeof(UInt32)).Take(sizeof(UInt32)).Reverse().ToArray(), 0);
            return BitConverter.ToUInt32(data, index * sizeof(UInt32));
        }

        public UInt32 GetUInt32H(int index = 0) { return GetUInt32(); }

        public Int16 GetInt16(int index = 0)
        {
            if (index >= data.Length / sizeof(Int16)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToInt16(data.Skip(index * sizeof(Int16)).Take(sizeof(Int16)).Reverse().ToArray(), 0);
            return BitConverter.ToInt16(data, index * sizeof(Int16));
        }

        public UInt16 GetUInt16(int index = 0)
        {
            if (index >= data.Length / sizeof(UInt16)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToUInt16(data.Skip(index * sizeof(UInt16)).Take(sizeof(UInt16)).Reverse().ToArray(), 0);
            return BitConverter.ToUInt16(data, index * sizeof(UInt16));
        }

        public Single GetSingle(int index = 0)
        {
            if (index >= data.Length / sizeof(Single)) throw new ArgumentOutOfRangeException();
            if (n64Data) return BitConverter.ToSingle(data.Skip(index * sizeof(Single)).Take(sizeof(Single)).Reverse().ToArray(), 0);
            return BitConverter.ToSingle(data, index * sizeof(Single));
        }

        public string GetString(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            string retVal = Encoding.ASCII.GetString(data);
            return retVal.IndexOf('\0') > -1 ? retVal.Substring(0, retVal.IndexOf('\0')) : retVal;
        }

        public Vector3D GetVector3D(int index = 0)
        {
            if (index >= data.Length / sizeof(Single) / 3) throw new ArgumentOutOfRangeException();
            return new Vector3D() { x = GetSingle(index * 3), y = GetSingle(index * 3 + 1), z = GetSingle(index * 3 + 2) };
        }

        public Vector2D GetVector2D(int index = 0)
        {
            if (index >= data.Length / sizeof(Single) / 2) throw new ArgumentOutOfRangeException();
            return new Vector2D() { x = GetSingle(index * 2), z = GetSingle(index * 2 + 1) };
        }

        public Matrix GetMatrix(int index = 0)
        {
            return new Matrix()
            {
                right = GetVector3D(index * 4 + 0),
                up    = GetVector3D(index * 4 + 1),
                front = GetVector3D(index * 4 + 2),
                posit = GetVector3D(index * 4 + 3)
            };
        }

        public Euler GetEuler(int index = 0)
        {
            /*int EulerSize = (sizeof(Single) * 6) + (sizeof(Single) * 3 * 3);
            if (n64Data)
            {
                return new Euler()
                {
                    mass      = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 00)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    mass_inv  = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 01)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    v_mag     = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 02)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    v_mag_inv = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 03)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    I         = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 04)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    k_i       = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 05)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                    v = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 06)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            y = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 07)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            z = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 08)).Take(sizeof(Single)).Reverse().ToArray(), 0)
                        },
                    omega = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 09)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            y = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 10)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            z = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 11)).Take(sizeof(Single)).Reverse().ToArray(), 0)
                        },
                    Accel = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 12)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            y = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 13)).Take(sizeof(Single)).Reverse().ToArray(), 0),
                            z = BitConverter.ToSingle(data.Skip((index * EulerSize) + (sizeof(Single) * 14)).Take(sizeof(Single)).Reverse().ToArray(), 0)
                        }
                };
            }
            else
            {
                return new Euler()
                {
                    mass      = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 00)),
                    mass_inv  = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 01)),
                    v_mag     = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 02)),
                    v_mag_inv = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 03)),
                    I         = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 04)),
                    k_i       = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 05)),
                    v = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 06)),
                            y = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 07)),
                            z = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 08)),
                        },
                    omega = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 09)),
                            y = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 10)),
                            z = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 11)),
                        },
                    Accel = new Vector3D()
                        {
                            x = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 12)),
                            y = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 13)),
                            z = BitConverter.ToSingle(data, (index * EulerSize) + (sizeof(Single) * 14)),
                        }
                };
            }*/
            throw new NotImplementedException();
        }

        public bool IsValidationOnly() { return false; }

        public override string ToString()
        {
            if (n64Data)
                return "BINARY\tType: UNKNOWN (N64 MODE)";
            return "BINARY\tType: " + type;
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            if (n64Data) return true;
            return this.type == type;
        }
    }

    public class BZNStringToken : IBZNToken, IBZNStringToken
    {
        private string value;
        private string name;

        public BZNStringToken(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public bool GetBoolean(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return bool.Parse(value);
        }

        public Int32 GetInt32(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return Int32.Parse(value);
        }

        public UInt32 GetUInt32(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return UInt32.Parse(value);
        }

        public UInt32 GetUInt32H(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return UInt32.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        public Int16 GetInt16(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return Int16.Parse(value);
        }

        public UInt16 GetUInt16(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return UInt16.Parse(value);
        }

        public float GetSingle(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return Single.Parse(value);
        }

        public string GetString(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return value;
        }

        public Vector3D GetVector3D(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Vector2D GetVector2D(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Matrix GetMatrix(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Euler GetEuler(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public bool IsValidationOnly() { return false; }

        public override string ToString()
        {
            return "ASCII\tName:" + name;
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            return this.name == name;
        }
    }

    public class BZNStringArrayToken : IBZNToken, IBZNStringToken
    {
        private string[] values;
        private string name;

        public BZNStringArrayToken(string name, string[] values)
        {
            this.name = name;
            this.values = values;
        }

        public bool GetBoolean(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return bool.Parse(values[index]);
        }

        public Int32 GetInt32(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return Int32.Parse(values[index]);
        }

        public UInt32 GetUInt32(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return UInt32.Parse(values[index]);
        }

        public UInt32 GetUInt32H(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return UInt32.Parse(values[index], System.Globalization.NumberStyles.HexNumber);
        }

        public Int16 GetInt16(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return Int16.Parse(values[index]);
        }

        public UInt16 GetUInt16(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return UInt16.Parse(values[index]);
        }

        public float GetSingle(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            return Single.Parse(values[index]);
        }

        public string GetString(int index = 0)
        {
            if (index > 0) throw new ArgumentOutOfRangeException();
            return values[0];
        }

        public Vector3D GetVector3D(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Vector2D GetVector2D(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Matrix GetMatrix(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Euler GetEuler(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public bool IsValidationOnly() { return false; }

        public override string ToString()
        {
            return "ASCII\tName:" + name;
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            return this.name == name;
        }
    }

    public class BZNStringComplexToken : IBZNToken, IBZNStringToken
    {
        private string name;
        private IBZNStringToken[][] values;

        public BZNStringComplexToken(string name, IBZNStringToken[][] values)
        {
            this.name = name;
            this.values = values;
        }

        public bool GetBoolean(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Int32 GetInt32(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public UInt32 GetUInt32(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public UInt32 GetUInt32H(int index = 0) { return GetUInt32(index); }

        public Int16 GetInt16(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public UInt16 GetUInt16(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public string GetString(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public float GetSingle(int index = 0)
        {
            throw new InvalidOperationException();
        }

        public Vector3D GetVector3D(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            IBZNStringToken[] subToks = values[index];

            if (!subToks[0].Validate("x")) throw new Exception("Failed to parse x");
            if (!subToks[1].Validate("y")) throw new Exception("Failed to parse y");
            if (!subToks[2].Validate("z")) throw new Exception("Failed to parse z");

            return new Vector3D() { x = subToks[0].GetSingle(), y = subToks[1].GetSingle(), z = subToks[2].GetSingle() };
        }

        public Vector2D GetVector2D(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            IBZNStringToken[] subToks = values[index];

            if (!subToks[0].Validate("x")) throw new Exception("Failed to parse x");
            if (!subToks[1].Validate("z")) throw new Exception("Failed to parse z");

            return new Vector2D() { x = subToks[0].GetSingle(), z = subToks[1].GetSingle() };
        }

        public Matrix GetMatrix(int index = 0)
        {
            IBZNStringToken[] subToks = values[index];
            if (!subToks[00].Validate("right_x")) throw new Exception("Failed to parse right_x");
            if (!subToks[01].Validate("right_y")) throw new Exception("Failed to parse right_y");
            if (!subToks[02].Validate("right_z")) throw new Exception("Failed to parse right_z");
            if (!subToks[03].Validate("up_x")) throw new Exception("Failed to parse up_x");
            if (!subToks[04].Validate("up_y")) throw new Exception("Failed to parse up_y");
            if (!subToks[05].Validate("up_z")) throw new Exception("Failed to parse up_z");
            if (!subToks[06].Validate("front_x")) throw new Exception("Failed to parse front_x");
            if (!subToks[07].Validate("front_y")) throw new Exception("Failed to parse front_y");
            if (!subToks[08].Validate("front_z")) throw new Exception("Failed to parse front_z");
            if (!subToks[09].Validate("posit_x")) throw new Exception("Failed to parse posit_x");
            if (!subToks[10].Validate("posit_y")) throw new Exception("Failed to parse posit_y");
            if (!subToks[11].Validate("posit_z")) throw new Exception("Failed to parse posit_z");

            return new Matrix()
            {
                right = new Vector3D() { x = subToks[00].GetSingle(), y = subToks[01].GetSingle(), z = subToks[02].GetSingle() },
                up    = new Vector3D() { x = subToks[03].GetSingle(), y = subToks[04].GetSingle(), z = subToks[05].GetSingle() },
                front = new Vector3D() { x = subToks[06].GetSingle(), y = subToks[07].GetSingle(), z = subToks[08].GetSingle() },
                posit = new Vector3D() { x = subToks[09].GetSingle(), y = subToks[10].GetSingle(), z = subToks[11].GetSingle() }
            };
        }

        public Euler GetEuler(int index = 0)
        {
            IBZNStringToken[] subToks = values[index];
            if (!subToks[0].Validate("mass")) throw new Exception("Failed to parse mass");
            if (!subToks[1].Validate("mass_inv")) throw new Exception("Failed to parse mass_inv");
            if (!subToks[2].Validate("v_mag")) throw new Exception("Failed to parse v_mag");
            if (!subToks[3].Validate("v_mag_inv")) throw new Exception("Failed to parse v_mag_inv");
            if (!subToks[4].Validate("I")) throw new Exception("Failed to parse I");
            if (!subToks[5].Validate("k_i")) throw new Exception("Failed to parse k_i");
            if (!subToks[6].Validate("v")) throw new Exception("Failed to parse v");
            if (!subToks[7].Validate("omega")) throw new Exception("Failed to parse omega");
            if (!subToks[8].Validate("Accel")) throw new Exception("Failed to parse Accel");

            return new Euler()
            {
                mass = subToks[0].GetSingle(),
                mass_inv = subToks[1].GetSingle(),
                v_mag = subToks[2].GetSingle(),
                v_mag_inv = subToks[3].GetSingle(),
                I = subToks[4].GetSingle(),
                k_i = subToks[5].GetSingle(),
                v = subToks[6].GetVector3D(),
                omega = subToks[7].GetVector3D(),
                Accel = subToks[8].GetVector3D()
            };
        }

        public bool IsValidationOnly() { return false; }

        public override string ToString()
        {
            return "ASCII\tName:" + name;
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            return this.name == name;
        }
    }

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
            IBZNToken BinaryToken = ReadToken();

            this._Version = VersionToken.GetInt32();
            this.inBinary = BinaryToken.GetBoolean();

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
            return null;
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

        private IBZNStringToken ReadStringValueSub(Stream filestream, string rawLine)
        {
            string[] line = rawLine.Split(' ');

            if (line[1] == "=")
            {
                string name = line[0];
                //bool isArray = false;

                if (ComplexStringTokenSizeMap.ContainsKey(name))
                {
                    int count = 1;

                    IBZNStringToken[][] values = new IBZNStringToken[count][];
                    for (int subSectionCounter = 0; subSectionCounter < count; subSectionCounter++)
                    {
                        values[subSectionCounter] = new IBZNStringToken[ComplexStringTokenSizeMap[name]];
                        for (int constructCounter = 0; constructCounter < ComplexStringTokenSizeMap[name]; constructCounter++)
                        {
                            string rawLineInner = ReadLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueSub(filestream, rawLineInner);
                        }
                    }

                    return new BZNStringComplexToken(name, values);
                }
                else
                {
                    string value = line[2];

                    //Console.WriteLine("ASCII\t\"{0}\"\tValidation: {1}", value, name);

                    return new BZNStringToken(name, value);
                }
            }
            else if (line[2] == "=")
            {
                string name = line[0];
                //bool isArray = true;
                int count = int.Parse(line[1].Substring(1, line[1].Length - 2));

                if (count == 0) return new BZNStringArrayToken(name, new string[0]);
                
                if (ComplexStringTokenSizeMap.ContainsKey(name))
                {
                    IBZNStringToken[][] values = new IBZNStringToken[count][];
                    for (int subSectionCounter = 0; subSectionCounter < count; subSectionCounter++)
                    {
                        values[subSectionCounter] = new IBZNStringToken[ComplexStringTokenSizeMap[name]];
                        for (int constructCounter = 0; constructCounter < ComplexStringTokenSizeMap[name]; constructCounter++)
                        {
                            string rawLineInner = ReadLine(filestream).TrimEnd('\r', '\n').TrimStart();
                            values[subSectionCounter][constructCounter] = ReadStringValueSub(filestream, rawLineInner);
                        }
                    }

                    return new BZNStringComplexToken(name, values);
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

                    return new BZNStringArrayToken(name, values);
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

            return new BZNBinaryToken((BinaryFieldType)type, data, n64Data);
        }

        public void Dispose()
        {
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
