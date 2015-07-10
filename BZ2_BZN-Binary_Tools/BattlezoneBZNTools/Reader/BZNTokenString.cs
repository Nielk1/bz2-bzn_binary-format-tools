using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public class BZNTokenString : IBZNToken, IBZNStringToken
    {
        private string value;
        private string name;

        public BZNTokenString(string name, string value)
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
}
