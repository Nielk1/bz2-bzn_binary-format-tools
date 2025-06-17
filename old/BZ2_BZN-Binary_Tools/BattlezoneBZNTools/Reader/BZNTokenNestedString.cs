using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public class BZNTokenNestedString : IBZNToken
    {
        private string name;
        private IBZNToken[][] values;

        public BZNTokenNestedString(string name, IBZNToken[][] values)
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
            IBZNToken[] subToks = values[index];

            if (!subToks[0].Validate("x")) throw new Exception("Failed to parse x");
            if (!subToks[1].Validate("y")) throw new Exception("Failed to parse y");
            if (!subToks[2].Validate("z")) throw new Exception("Failed to parse z");

            return new Vector3D() { x = subToks[0].GetSingle(), y = subToks[1].GetSingle(), z = subToks[2].GetSingle() };
        }

        public Vector2D GetVector2D(int index = 0)
        {
            if (index >= values.Length) throw new ArgumentOutOfRangeException();
            IBZNToken[] subToks = values[index];

            if (!subToks[0].Validate("x")) throw new Exception("Failed to parse x");
            if (!subToks[1].Validate("z")) throw new Exception("Failed to parse z");

            return new Vector2D() { x = subToks[0].GetSingle(), z = subToks[1].GetSingle() };
        }

        public Matrix GetMatrix(int index = 0)
        {
            IBZNToken[] subToks = values[index];
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
                up = new Vector3D() { x = subToks[03].GetSingle(), y = subToks[04].GetSingle(), z = subToks[05].GetSingle() },
                front = new Vector3D() { x = subToks[06].GetSingle(), y = subToks[07].GetSingle(), z = subToks[08].GetSingle() },
                posit = new Vector3D() { x = subToks[09].GetSingle(), y = subToks[10].GetSingle(), z = subToks[11].GetSingle() }
            };
        }

        public Euler GetEuler(int index = 0)
        {
            IBZNToken[] subToks = values[index];
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

        public byte[] GetRaw(int index = 0, int length = 1) { throw new InvalidOperationException(); }

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
