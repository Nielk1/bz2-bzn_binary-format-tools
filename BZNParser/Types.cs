using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser
{
    // note, there are some oddities with passing these around, might need to swap to classes unless this lets me use sizeof
    public struct Vector3D
    {
        public float x;
        public float y;
        public float z;
    }

    public struct Vector2D
    {
        public float x;
        public float z;
    }

    public struct Euler
    {
        public float mass;
        public float mass_inv;

        public float v_mag;
        public float v_mag_inv;

        public float I;
        public float k_i;

        public Vector3D v;
        public Vector3D omega;
        public Vector3D Accel;
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
