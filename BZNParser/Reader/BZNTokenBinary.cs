﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Reader
{
    public class BZNTokenBinary : IBZNToken
    {
        private BinaryFieldType type;
        private byte[] data;
        private bool IsBigEndian;

        public BZNTokenBinary(BinaryFieldType fIELD_TYPE, byte[] data, bool IsBigEndian)
        {
            // TODO: Complete member initialization
            this.type = fIELD_TYPE;
            this.data = data;
            this.IsBigEndian = IsBigEndian;
        }

        public bool GetBoolean(int index = 0)
        {
            if (index >= data.Length / sizeof(bool)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToBoolean(data.Skip(index * sizeof(bool)).Take(sizeof(bool)).Reverse().ToArray(), 0);
            return BitConverter.ToBoolean(data, index * sizeof(bool));
        }

        public Int32 GetInt32(int index = 0)
        {
            if (index >= data.Length / sizeof(Int32)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToInt32(data.Skip(index * sizeof(Int32)).Take(sizeof(Int32)).Reverse().ToArray(), 0);
            return BitConverter.ToInt32(data, index * sizeof(Int32));
        }

        public UInt32 GetUInt32(int index = 0)
        {
            if (index >= data.Length / sizeof(UInt32)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToUInt32(data.Skip(index * sizeof(UInt32)).Take(sizeof(UInt32)).Reverse().ToArray(), 0);
            return BitConverter.ToUInt32(data, index * sizeof(UInt32));
        }

        public UInt32 GetUInt32H(int index = 0)
        {
            return GetUInt32(index);
        }

        public UInt32 GetUInt32Raw(int index = 0)
        {
            return GetUInt32(index);
        }

        public UInt32 GetUInt32N64Fix(int index = 0)
        {
            if (index >= data.Length / sizeof(UInt32)) throw new ArgumentOutOfRangeException();
            //if (n64Data) return BitConverter.ToUInt32(data.Skip(index * sizeof(UInt32)).Take(sizeof(UInt32)).Reverse().ToArray(), 0);
            return BitConverter.ToUInt32(data, index * sizeof(UInt32));
        }

        public Int16 GetInt16(int index = 0)
        {
            if (index >= data.Length / sizeof(Int16)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToInt16(data.Skip(index * sizeof(Int16)).Take(sizeof(Int16)).Reverse().ToArray(), 0);
            return BitConverter.ToInt16(data, index * sizeof(Int16));
        }

        public UInt16 GetUInt16(int index = 0)
        {
            if (index >= data.Length / sizeof(UInt16)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToUInt16(data.Skip(index * sizeof(UInt16)).Take(sizeof(UInt16)).Reverse().ToArray(), 0);
            return BitConverter.ToUInt16(data, index * sizeof(UInt16));
        }

        public UInt16 GetUInt16H(int index = 0)
        {
            return GetUInt16(index);
        }

        public SByte GetInt8(int index = 0)
        {
            if (index >= data.Length / sizeof(byte)) throw new ArgumentOutOfRangeException();
            return (SByte)data[index];
        }

        public byte GetUInt8(int index = 0)
        {
            if (index >= data.Length / sizeof(byte)) throw new ArgumentOutOfRangeException();
            return data[index];
        }

        public Single GetSingle(int index = 0)
        {
            if (index >= data.Length / sizeof(Single)) throw new ArgumentOutOfRangeException();
            if (IsBigEndian) return BitConverter.ToSingle(data.Skip(index * sizeof(Single)).Take(sizeof(Single)).Reverse().ToArray(), 0);
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

        public Matrix GetMatrixOld(int index = 0)
        {
            return new Matrix()
            {
                right = GetVector3D(index * 4 + 0), rightw = 0,
                up    = GetVector3D(index * 4 + 1), upw    = 0,
                front = GetVector3D(index * 4 + 2), frontw = 0,
                posit = GetVector3D(index * 4 + 3), positw = 1
            };
        }
        public Matrix GetMatrix(int index = 0)
        {
            return new Matrix()
            {
                right  = new Vector3D() { x = GetSingle(index * 16 +  0),
                                          y = GetSingle(index * 16 +  1),
                                          z = GetSingle(index * 16 +  2) },
                rightw =                      GetSingle(index * 16 +  3),
                up     = new Vector3D() { x = GetSingle(index * 16 +  4),
                                          y = GetSingle(index * 16 +  5),
                                          z = GetSingle(index * 16 +  6) },
                upw    =                      GetSingle(index * 16 +  7),
                front  = new Vector3D() { x = GetSingle(index * 16 +  8),
                                          y = GetSingle(index * 16 +  9),
                                          z = GetSingle(index * 16 + 10) },
                frontw =                      GetSingle(index * 16 + 11),
                posit  = new Vector3D() { x = GetSingle(index * 16 + 12),
                                          y = GetSingle(index * 16 + 13),
                                          z = GetSingle(index * 16 + 14) },
                positw =                      GetSingle(index * 16 + 15)
            };
        }

        public Euler GetEuler(int index = 0)
        {
            throw new NotImplementedException();
        }

        public byte[] GetBytes(int index = 0, int length = -1)
        {
            if (index + length > data.Length) throw new ArgumentOutOfRangeException();
            return data.Skip(index).Take(length).ToArray();
        }
        public byte[] GetRaw(int index = 0, int length = -1)
        {
            if (length == -1) return data.Skip(index).ToArray();
            return data.Skip(index).Take(length).ToArray();
        }

        public bool IsValidationOnly() { return false; }

        public override string ToString()
        {
            switch (type)
            {
                case BinaryFieldType.DATA_CHAR:
                    {
                        string str = GetString();
                        if (str.Any(dr => char.IsControl(dr)))
                            return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToString(data.Take(20).ToArray())}{(data.Length > 20 ? "..." : string.Empty)}";
                        return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: \"{str}\"";
                    }
                case BinaryFieldType.DATA_SHORT: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToInt16(data, 0)}";
                case BinaryFieldType.DATA_LONG: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToInt32(data, 0)}";
                case BinaryFieldType.DATA_FLOAT: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToSingle(data, 0)}";
                case BinaryFieldType.DATA_DOUBLE: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToDouble(data, 0)}";
                case BinaryFieldType.DATA_ID: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToUInt32(data, 0):X8}";
                case BinaryFieldType.DATA_PTR: return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToUInt32(data, 0):X8}";
                case BinaryFieldType.DATA_VEC2D:
                    {
                        Vector2D v = GetVector2D();
                        return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {{ {v.x}, {v.z} }}";
                    }
                case BinaryFieldType.DATA_VEC3D:
                    {
                        Vector3D v = GetVector3D();
                        return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {{ {v.x}, {v.y}, {v.z} }}";
                    }
                case BinaryFieldType.DATA_MAT3DOLD:
                    {
                        Matrix m = GetMatrixOld();
                        return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {{ {{ {m.right.x,10:0.00}, {m.right.y,10:0.00}, {m.right.z,10:0.00} }},\r\n" +
                               $"      \t                   \t         {{ {m.up.x,10:0.00}, {m.up.y,10:0.00}, {m.up.z,10:0.00} }},\r\n" +
                               $"      \t                   \t         {{ {m.front.x,10:0.00}, {m.front.y,10:0.00}, {m.front.z,10:0.00} }},\r\n" +
                               $"      \t                   \t         {{ {m.posit.x,10:0.00}, {m.posit.y,10:0.00}, {m.posit.z,10:0.00} }} }}";
                    }
                case BinaryFieldType.DATA_MAT3D:
                    {
                        Matrix m = GetMatrix();
                        return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {{ {{ {m.right.x,10:0.00}, {m.right.y,10:0.00}, {m.right.z,10:0.00}, {m.rightw,10:0.00} }},\r\n" +
                                $"      \t                   \t         {{ {m.up.x,10:0.00}, {m.up.y,10:0.00}, {m.up.z,10:0.00}, {m.upw,10:0.00} }},\r\n" +
                                $"      \t                   \t         {{ {m.front.x,10:0.00}, {m.front.y,10:0.00}, {m.front.z,10:0.00}, {m.frontw,10:0.00} }},\r\n" +
                                $"      \t                   \t         {{ {m.posit.x,10:0.00}, {m.posit.y,10:0.00}, {m.posit.z,10:0.00}, {m.positw,10:0.00} }} }}";
                    }
            }
            return $"BINARY\tType: {type.ToString().PadRight(13)}\tValue: {BitConverter.ToString(data.Take(20).ToArray())}{(data.Length > 20 ? "..." : string.Empty)}";
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            if (this.type == BinaryFieldType.DATA_UNKNOWN) return true;
            return this.type == type;
        }
    }
}
