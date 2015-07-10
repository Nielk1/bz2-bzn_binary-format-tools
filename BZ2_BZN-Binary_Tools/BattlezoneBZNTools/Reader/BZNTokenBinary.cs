using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public class BZNTokenBinary : IBZNToken
    {
        private BinaryFieldType type;
        private byte[] data;
        private bool n64Data;

        public BZNTokenBinary(BinaryFieldType fIELD_TYPE, byte[] data, bool n64Data)
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
                up = GetVector3D(index * 4 + 1),
                front = GetVector3D(index * 4 + 2),
                posit = GetVector3D(index * 4 + 3)
            };
        }

        public Euler GetEuler(int index = 0)
        {
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
}
