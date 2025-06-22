using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;

namespace BZNParser
{
    // note, there are some oddities with passing these around, might need to swap to classes unless this lets me use sizeof
    public struct Vector3D
    {
        public float x;
        public float y;
        public float z;

        internal float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public struct Vector2D
    {
        public float x;
        public float z;
    }

    public struct Euler
    {
        const float EPSILON = 1.0e-4f;
        const float HUGE_NUMBER = 1.0e30f;

        public Quaternion Att;

        public Vector3D v;
        public Vector3D omega;
        public Vector3D Accel;

        public Vector3D Alpha;
        public Vector3D Pos;

        public float mass;
        public float mass_inv;

        public float I;
        public float I_inv;

        public float v_mag;
        public float v_mag_inv;

        public void InitLoadSave()
        {
            v = new Vector3D();
            omega = new Vector3D();
            Accel = new Vector3D();
            Alpha = new Vector3D();
        }

        // Reads the 'mass' member, builds mass_inv and I_inv fields
        public void CalcMassIInv()
        {
            mass_inv = HUGE_NUMBER;
            I_inv = HUGE_NUMBER;
            if (mass > EPSILON)
            {
                mass_inv = 1.0f / mass;
                I_inv = 1.0f / I;
            }
            else
            {
                mass_inv = HUGE_NUMBER;
                I_inv = HUGE_NUMBER;
            }
        }


        // Reads the 'v' member, builds the 'v_mag' and 'v_mag_inv' members
        public void CalcVMag()
        {
            v_mag = v.Magnitude();
            v_mag_inv = (v_mag == 0.0f) ? HUGE_NUMBER : 1.0f / v_mag;
        }
    }

    /*public struct MatrixOld
    {
        public Vector3D right;
        public Vector3D up;
        public Vector3D front;
        public Vector3D posit;
    }*/

    public struct Matrix
    {
        public Vector3D right;
        public float rightw;
        public Vector3D up;
        public float upw;
        public Vector3D front;
        public float frontw;
        public Vector3D posit;
        public float positw;
    }

    public struct Quaternion
    {
    }
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

    public enum BZNFormat
    {
        Battlezone,
        Battlezone2,
        BattlezoneN64,
        StarTrekArmada,
        StarTrekArmada2,
    }
}
