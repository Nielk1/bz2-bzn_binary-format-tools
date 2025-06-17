using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public class BZNTokenString : IBZNToken
    {
        private string[] values;
        private string name;

        public BZNTokenString(string name, string[] values)
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

        public byte[] GetRaw(int index = 0, int length = 1) {
            if (index + length > values[0].Length / 2) throw new ArgumentOutOfRangeException();
            char[] rawDataArray = values[0].Skip(index * 2).Take(length * 2).ToArray();
            byte[] dataOut = new byte[rawDataArray.Length / 2];
            for(int x=0;x<dataOut.Length;x++)
            {
                dataOut[x] = byte.Parse("" + rawDataArray[x * 2 + 0] + rawDataArray[x * 2 + 1]);
            }
            return dataOut;
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
