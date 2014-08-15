using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace BinaryBZNFile
{
    public enum FIELD_TYPE : byte
    {
        DATA_VOID     = 0, //0x00
        DATA_BOOL     = 1, //0x01
        DATA_CHAR     = 2, //0x02
        DATA_SHORT    = 3, //0x03
        DATA_LONG     = 4, //0x04
        DATA_FLOAT    = 5, //0x05
        DATA_DOUBLE   = 6, //0x06
        DATA_ID       = 7, //0x07
        DATA_PTR      = 8, //0x08
        DATA_VEC3D    = 9, //0x09
        DATA_VEC2D    = 10,//0x0A
        DATA_MAT3DOLD = 11,//0x0B
        DATA_MAT3D    = 12,//0x0C
        DATA_STRING   = 13,//0x0D
        DATA_QUAT     = 14 //0x0E
    }

    public interface IBinaryBZN
    {
        List<Field> fields { get; set; }
        void save(System.IO.FileStream fileStream);
    }

    public class BinaryBZN : IBinaryBZN
    {
        //public string VersionField;
        //public string Version;
        //public string SaveTypeField;
        //public string SaveType;
        //public string BinarySaveField;
        //public string BinarySave;
        public List<byte> StringPart;
        public List<Field> fields { get; set; }

        private bool bz1 = false;

        public BinaryBZN(Stream filestream, bool bz1 = false)
        {
            //VersionField = "";
            //Version = "";
            //SaveTypeField = "";
            //SaveType = "";
            //BinarySaveField = "";
            //BinarySave = "";
            StringPart = new List<byte>();
            //List<char> tmpString = new List<char>();
            fields = new List<Field>();

            // this reading code is awful btw
            byte[] readByte = new byte[1];
            //bool saw0D = false;
            //int readingLine = 0;
            List<byte> tmpBytes = new List<byte>();

            bool InBinaryData = false;

            this.bz1 = bz1;

            while (filestream.Read(readByte, 0, 1) > 0)
            {
                byte byteX = readByte[0];
                if (InBinaryData)
                {
                    tmpBytes.Add(byteX);
                    //Console.WriteLine("Wrote Byte " + byteX.ToString("X"));
                }
                else
                {
                    if (!Char.IsControl((char)byteX))
                    {
                        //tmpString += (char)byteX;
                        tmpBytes.Add(byteX);
                        //Console.WriteLine("Wrote Char " + (char)byteX);
                    }
                    else
                    {
                        if (byteX == 0x0A || byteX == 0x0D)
                        {
                            //StringPart += tmpString + (char)byteX;
                            tmpBytes.ForEach(i => StringPart.Add(i));
                            StringPart.Add(byteX);
                            //tmpString = string.Empty;
                            tmpBytes.Clear(); // woops, don't need these bytes after all
                            //Console.WriteLine("Wrote Char " + (char)byteX);
                        }
                        else
                        {
                            tmpBytes.Add(byteX);
                            InBinaryData = true;
                            //Console.WriteLine("Wrote Bytes " + byteX.ToString("X"));
                        }
                    }
                }
            }
            byte[] dataBytes = tmpBytes.ToArray();

            //if (!BinarySave.ToLowerInvariant().Equals("true"))
            //{
            //    throw new Exception("Binary flag not \"true\".");
            //}

            int offset = 0;
            if (bz1) offset = 1;
            for (int x = 0; x < dataBytes.Length; )
            {
                ushort Size = (ushort)((dataBytes[x + 2 + offset] << 8) | dataBytes[x + 1 + offset]);
                int dataOffset = x + 3 + offset;

                switch (dataBytes[x])
                {
                    case 0: // DATA_VOID
                        fields.Add(new NamedField(FIELD_TYPE.DATA_VOID, "DATA_VOID", dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 1: // DATA_BOOL
                        fields.Add(new BoolField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        //fields.Add(new BoolField(dataBytes[x + 3] != 0x00));
                        break;
                    case 2: // DATA_CHAR
                        fields.Add(new CharField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 3: // DATA_SHORT
                        fields.Add(new ShortField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        //fields.Add(new ShortField((short)((dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        break;
                    case 4: // DATA_LONG
                        fields.Add(new LongField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        //fields.Add(new LongField((long)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        break;
                    case 5: // DATA_FLOAT
                        fields.Add(new FloatField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        //fields.Add(new FloatField(System.BitConverter.ToSingle(dataBytes, x + 3)));
                        break;
                    case 6: // DATA_DOUBLE
                        fields.Add(new DoubleField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        /*fields.Add(new DoubleField((double)(
                            (dataBytes[x + 10] << 64) |
                            (dataBytes[x + 9] << 56) |
                            (dataBytes[x + 8] << 48) |
                            (dataBytes[x + 7] << 40) |
                            (dataBytes[x + 6] << 32) |
                            (dataBytes[x + 5] << 16) |
                            (dataBytes[x + 4] << 8) |
                            dataBytes[x + 3])));*/
                        break;
                    case 7: // DATA_ID
                        //fields.Add(new UnknownField("DATA_ID", Size));
                        fields.Add(new NamedField(FIELD_TYPE.DATA_ID, "DATA_ID", dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 8: // DATA_PTR
                        //fields.Add(new PtrField((long)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        fields.Add(new PtrField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 9: // DATA_VEC3D
                        /*fields.Add(new Vec3DField(System.BitConverter.ToSingle(dataBytes, x + 3),
                                                  System.BitConverter.ToSingle(dataBytes, x + 7),
                                                  System.BitConverter.ToSingle(dataBytes, x + 11)));*/
                        fields.Add(new Vec3DField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 10: // DATA_VEC2D
                        /*fields.Add(new Vec2DField(System.BitConverter.ToSingle(dataBytes, x + 3),
                                                  System.BitConverter.ToSingle(dataBytes, x + 7)));*/
                        fields.Add(new Vec2DField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 11: // DATA_MAT3DOLD
                        //fields.Add(new UnknownField("DATA_MAT3DOLD", Size));
                        fields.Add(new NamedField(FIELD_TYPE.DATA_MAT3DOLD, "DATA_MAT3DOLD", dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 12: // DATA_MAT3D
                        /*fields.Add(new Mat3DField(System.BitConverter.ToSingle(dataBytes, x + 3),
                                                  System.BitConverter.ToSingle(dataBytes, x + 7),
                                                  System.BitConverter.ToSingle(dataBytes, x + 11),
                                                  System.BitConverter.ToSingle(dataBytes, x + 15),
                                                  System.BitConverter.ToSingle(dataBytes, x + 19),
                                                  System.BitConverter.ToSingle(dataBytes, x + 23),
                                                  System.BitConverter.ToSingle(dataBytes, x + 27),
                                                  System.BitConverter.ToSingle(dataBytes, x + 31),
                                                  System.BitConverter.ToSingle(dataBytes, x + 35),
                                                  System.BitConverter.ToSingle(dataBytes, x + 39),
                                                  System.BitConverter.ToSingle(dataBytes, x + 33),
                                                  System.BitConverter.ToSingle(dataBytes, x + 37),
                                                  System.BitConverter.ToSingle(dataBytes, x + 51),
                                                  System.BitConverter.ToSingle(dataBytes, x + 55),
                                                  System.BitConverter.ToSingle(dataBytes, x + 59),
                                                  System.BitConverter.ToSingle(dataBytes, x + 63)));*/
                        fields.Add(new Mat3DField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 13: // DATA_STRING
                        //fields.Add(new StringField(dataBytes.Skip(x + 3).Take(Size).ToArray()));
                        fields.Add(new NamedField(FIELD_TYPE.DATA_STRING, "DATA_STRING", dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    case 14: // DATA_QUAT
                        /*fields.Add(new QuatField((float)((dataBytes[x +  6] << (8 *  3)) | (dataBytes[x +  5] << (8 *  2)) | (dataBytes[x +  4] <<  8)       |  dataBytes[x + 3]),
                                                 (float)((dataBytes[x + 10] << (8 *  7)) | (dataBytes[x +  9] << (8 *  6)) | (dataBytes[x +  8] << (8 *  5)) | (dataBytes[x + 7] << (8 * 4))),
                                                 (float)((dataBytes[x + 14] << (8 * 11)) | (dataBytes[x + 13] << (8 * 10)) | (dataBytes[x + 12] << (8 *  9)) | (dataBytes[x + 11] << (8 * 8))),
                                                 (float)((dataBytes[x + 18] << (8 * 15)) | (dataBytes[x + 17] << (8 * 14)) | (dataBytes[x + 16] << (8 * 13)) | (dataBytes[x + 15] << (8 * 12)))));*/
                        fields.Add(new QuatField(dataBytes.Skip(dataOffset).Take(Size).ToArray()));
                        break;
                    //                    default:
                    //                        fields.Add(new Field(dataBytes.Skip(x + 3).Take(Size).ToArray()));
                    //                        break;
                }

                x += 3 + + offset + Size;
            }
        }

        public void save(FileStream fileStream)
        {
            fileStream.SetLength(0);
            fileStream.Write(StringPart.ToArray(), 0, StringPart.Count);
            for (int x = 0; x < fields.Count; x++)
            {
                byte[] fieldData = fields[x].GetRawRef();
                fileStream.WriteByte((byte)(fields[x].GetFieldType()));
                if (bz1) fileStream.WriteByte((byte)0);
                fileStream.Write(System.BitConverter.GetBytes((short)(fieldData.Length)), 0, 2);
                fileStream.Write(fieldData, 0, fieldData.Length);
            }
            fileStream.Close();
        }
    }

    public class N64BZN : IBinaryBZN
    {
        //public string VersionField;
        //public string Version;
        //public string SaveTypeField;
        //public string SaveType;
        //public string BinarySaveField;
        //public string BinarySave;
        public List<byte> StringPart;
        public List<Field> fields { get; set; }

        public N64BZN(Stream filestream)
        {
            //VersionField = "";
            //Version = "";
            //SaveTypeField = "";
            //SaveType = "";
            //BinarySaveField = "";
            //BinarySave = "";
            StringPart = new List<byte>();
            //List<char> tmpString = new List<char>();
            fields = new List<Field>();

            // this reading code is awful btw
            byte[] readByte = new byte[1];
            //bool saw0D = false;
            //int readingLine = 0;
            List<byte> tmpBytes = new List<byte>();

            while (filestream.Read(readByte, 0, 1) > 0)
            {
                byte byteX = readByte[0];
                tmpBytes.Add(byteX);
            }
            byte[] dataBytes = tmpBytes.ToArray();

            //if (!BinarySave.ToLowerInvariant().Equals("true"))
            //{
            //    throw new Exception("Binary flag not \"true\".");
            //}
            
            for (int x = 0; x < dataBytes.Length; )
            {
                ushort Size = (ushort)((dataBytes[x] << 8) | dataBytes[x + 1]);

                fields.Add(new NamedField(FIELD_TYPE.DATA_VOID, "UNKNOWN", dataBytes.Skip(x + 2).Take(Size).ToArray()));

                x += 2 + Size;
                if (Size % 2 != 0) x++; // deal with padding
            }
        }

        public void save(FileStream fileStream)
        {
            fileStream.SetLength(0);
            fileStream.Write(StringPart.ToArray(), 0, StringPart.Count);
            for (int x = 0; x < fields.Count; x++)
            {
                byte[] fieldData = fields[x].GetRawRef();
                fileStream.Write(System.BitConverter.GetBytes((short)(fieldData.Length)).Reverse().ToArray(), 0, 2);
                fileStream.Write(fieldData, 0, fieldData.Length);
                if (fieldData.Length % 2 != 0) fileStream.WriteByte((byte)0); // deal with padding
            }
            fileStream.Close();
        }
    }

    public class Field
    {
        protected byte[] data;
        protected FIELD_TYPE type;

        public Field(FIELD_TYPE _type, byte[] _data)
        {
            data = _data;
            type = _type;
        }

        public FIELD_TYPE GetFieldType()
        {
            return type;
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_FIELD:\t");
            retVal.Append(data.Length);
            retVal.Append("\t");
            
            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }

        public byte[] GetRawRef()
        {
            return data;
        }

        public void SetRawRef(byte[] p)
        {
            data = p;
        }
    }

    public class NamedField : Field
    {
        public byte[] Value
        {
            get { return data.ToArray(); }
            set { data = value.ToArray(); }
        }

        private string Name;

        public NamedField(FIELD_TYPE _type, string _name, byte[] _data)
            : base(_type, _data)
        {
            Name = _name;
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append(Name);
            retVal.Append(":\t");
            retVal.Append(data.Length);
            retVal.Append("\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(char.IsControl((char)data[x]) || data[x] == 0 ? "\\0x" + Convert.ToString(data[x], 16).PadLeft(2, '0') : "" + (char)data[x]);
            }

            retVal.Append("\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }

    /*public class UnknownField : Field
    {
        string name;
        ushort size;
        public UnknownField(string name, ushort size)
        {
            this.name = name;
            this.size = size;
        }

        public override string ToString()
        {
            return "?" + name + ": " + size;
        }

        public byte[] GetRawRef()
        {
            return null;
        }
    }*/
    public class BoolField : Field
    {
        public bool[] Value
        {
//            get { return data[0] != 0x00; }
            get
            {
                List<bool> retVal = new List<bool>();
                for (int offset = 0; offset < data.Length; offset += sizeof(bool))
                {
                    retVal.Add(data[offset] != 0x00);
                }
                return retVal.ToArray();
            }
//            set { data[0] = value ? (byte)1 : (byte)0; }
        }

        public BoolField(byte[] _data) : base(FIELD_TYPE.DATA_BOOL, _data) { }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_BOOL:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++) { retVal.Append(x > 0 ? ", " : "" + Value[x]); }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class CharField : Field
    {
        public char[] Value
        {
            get { return data.Cast<char>().ToArray(); }
            set { data = value.Cast<byte>().ToArray(); }
        }

        public CharField(byte[] _data) : base(FIELD_TYPE.DATA_CHAR, _data) { }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_CHAR:\t");
            retVal.Append(data.Length);
            retVal.Append("\t");
            
            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(char.IsControl((char)data[x]) || data[x] == 0 ? "\\0x" + Convert.ToString(data[x], 16).PadLeft(2, '0') : "" + (char)data[x]);
            }

            retVal.Append("\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class ShortField : Field
    {
        public short[] Value
        {
            //get { return (short)((data[1] << 8) | data[0]); }
            get
            {
                List<short> retVal = new List<short>();
                for (int offset = 0; offset < data.Length; offset += sizeof(short))
                {
                    retVal.Add(System.BitConverter.ToInt16(data, offset));
                }
                return retVal.ToArray();
            }
//            set
//            {
//                byte[] byteData = System.BitConverter.GetBytes(value);
//                for (int x = 0; x < byteData.Length; x++) { data[x] = byteData[x]; }
//            }
        }

        public ShortField(byte[] _data) : base(FIELD_TYPE.DATA_SHORT, _data) { }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_SHORT:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++) { retVal.Append(x > 0 ? ", " : "" + Value[x]); }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class LongField : Field
    {
        public Int32[] Value
        {
            get {
                List<Int32> retVal = new List<Int32>();
                for (int offset = 0; offset < data.Length; offset += sizeof(Int32))
                {
                    retVal.Add(System.BitConverter.ToInt32(data, offset));
                }
                return retVal.ToArray();
            }
//            set
//            {
//                byte[] byteData = System.BitConverter.GetBytes(value);
//                for (int x = 0; x < byteData.Length; x++) { data[x] = byteData[x]; }
//            }
        }

        public LongField(byte[] _data) : base(FIELD_TYPE.DATA_LONG, _data) { }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_LONG:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++) { retVal.Append(x > 0 ? ", " : "" + Value[x]); }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class FloatField : Field
    {
        public float[] Value
        {
            get {
                //return System.BitConverter.ToSingle(data, 0);
                List<float> retVal = new List<float>();
                for (int offset = 0; offset < data.Length; offset += sizeof(float))
                {
                    retVal.Add(System.BitConverter.ToSingle(data, offset));
                }
                return retVal.ToArray();
            }
//            set
//            {
//                byte[] byteData = System.BitConverter.GetBytes(value);
//                for (int x = 0; x < byteData.Length; x++) { data[x] = byteData[x]; }
//            }
        }

        public FloatField(byte[] _data) : base(FIELD_TYPE.DATA_FLOAT, _data) { }

        public override string ToString()
        {
            //return "DATA_FLOAT: " + value.ToString("G");

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_FLOAT:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++) { retVal.Append(x > 0 ? ", " : "" + Value[x]); }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class DoubleField : Field
    {
        public double[] Value
        {
            //get { return System.BitConverter.ToDouble(data, 0); }
            get
            {
                List<double> retVal = new List<double>();
                for (int offset = 0; offset < data.Length; offset += sizeof(double))//2)
                {
                    retVal.Add(System.BitConverter.ToDouble(data, offset));
                }
                return retVal.ToArray();
            }
//            set
//            {
//                byte[] byteData = System.BitConverter.GetBytes(value);
//                for (int x = 0; x < byteData.Length; x++) { data[x] = byteData[x]; }
//            }
        }

        public DoubleField(byte[] _data) : base(FIELD_TYPE.DATA_DOUBLE, _data) { }

        public override string ToString()
        {
            //return "DATA_DOUBLE: " + value.ToString("G");

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_DOUBLE:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++) { retVal.Append(x > 0 ? ", " : "" + Value[x]); }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class PtrField : Field
    {
        public Int32 Value
        {
            get { return System.BitConverter.ToInt32(data, 0); }
            set
            {
                byte[] byteData = System.BitConverter.GetBytes(value);
                for (int x = 0; x < byteData.Length; x++) { data[x] = byteData[x]; }
            }
        }

        public PtrField(byte[] _data) : base(FIELD_TYPE.DATA_PTR, _data) { }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_PTR:\t");
            retVal.Append(data.Length);

            retVal.Append("\t");
            retVal.Append(Value);
            retVal.Append("\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }

    public class Vec3DField : Field
    {
        public float[][] Value
        {
//            get { return new float[] {
//                System.BitConverter.ToSingle(data, 0),
//                System.BitConverter.ToSingle(data, 4),
//                System.BitConverter.ToSingle(data, 8)
//            }; }
            get
            {
                List<float[]> retVal = new List<float[]>();
                for (int offset = 0; offset < data.Length; offset += (sizeof(float) * 3))
                {
                    //retVal.Add(System.BitConverter.ToInt16(data, offset));
                    retVal.Add(new float[] {
                        System.BitConverter.ToSingle(data, offset + 0),
                        System.BitConverter.ToSingle(data, offset + 4),
                        System.BitConverter.ToSingle(data, offset + 8)
                    });
                }
                return retVal.ToArray();
            }
//            set
//            {
//                for (int x = 0; x < value.Length; x++) {
//                    byte[] tmpData = System.BitConverter.GetBytes(value[x]);
//                    for (int y = 0; y < 4; y++)
//                    {
//                        value[(x * 4) + y] = tmpData[y];
//                    }
//                }
//            }
        }

        public Vec3DField(byte[] _data) : base(FIELD_TYPE.DATA_VEC3D, _data) { }

        public override string ToString()
        {
            //return "DATA_VEC3D: {" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "}";

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_VEC3D:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++)
            {
                retVal.Append(x > 0 ? ", " : "");
                retVal.Append("{");
                retVal.Append(Value[x][0]);
                retVal.Append(",");
                retVal.Append(Value[x][1]);
                retVal.Append(",");
                retVal.Append(Value[x][2]);
                retVal.Append("}");
            }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }

    public class Vec2DField : Field
    {
        public float[][] Value
        {
//            get
//            {
//                return new float[] {
//                    System.BitConverter.ToSingle(data, 0),
//                    System.BitConverter.ToSingle(data, 4)
//                };
//            }
            get
            {
                List<float[]> retVal = new List<float[]>();
                for (int offset = 0; offset < data.Length; offset += (sizeof(float) * 2))
                {
                    retVal.Add(new float[] {
                        System.BitConverter.ToSingle(data, offset + 0),
                        System.BitConverter.ToSingle(data, offset + 4)
                    });
                }
                return retVal.ToArray();
            }
//            set
//            {
//                for (int x = 0; x < value.Length; x++)
//                {
//                    byte[] tmpData = System.BitConverter.GetBytes(value[x]);
//                    for (int y = 0; y < 4; y++)
//                    {
//                        value[(x * 4) + y] = tmpData[y];
//                    }
//                }
//            }
        }

        public Vec2DField(byte[] _data) : base(FIELD_TYPE.DATA_VEC2D, _data) { }

        public override string ToString()
        {
            //return "DATA_VEC3D: {" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "}";

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_VEC2D:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++)
            {
                retVal.Append(x > 0 ? ", " : "");
                retVal.Append("{");
                retVal.Append(Value[x][0]);
                retVal.Append(",");
                retVal.Append(Value[x][1]);
                retVal.Append("}");
            }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }
    public class Mat3DField : Field
    {
        public float[][] Value
        {
//            get
//            {
//                return new float[] {
//                    System.BitConverter.ToSingle(data, 0*4),
//                    System.BitConverter.ToSingle(data, 1*4),
//                    System.BitConverter.ToSingle(data, 2*4),
//                    System.BitConverter.ToSingle(data, 3*4),
//                    System.BitConverter.ToSingle(data, 4*4),
//                    System.BitConverter.ToSingle(data, 5*4),
//                    System.BitConverter.ToSingle(data, 6*4),
//                    System.BitConverter.ToSingle(data, 7*4),
//                    System.BitConverter.ToSingle(data, 8*4),
//                    System.BitConverter.ToSingle(data, 9*4),
//                    System.BitConverter.ToSingle(data, 10*4),
//                    System.BitConverter.ToSingle(data, 11*4),
//                    System.BitConverter.ToSingle(data, 12*4),
//                    System.BitConverter.ToSingle(data, 13*4),
//                    System.BitConverter.ToSingle(data, 14*4),
//                    System.BitConverter.ToSingle(data, 15*4)
//                };
//            }
            get
            {
                List<float[]> retVal = new List<float[]>();
                for (int offset = 0; offset < data.Length; offset += (sizeof(float) * 16))
                {
                    retVal.Add(new float[] {
                        System.BitConverter.ToSingle(data, offset + (0*4)),
                        System.BitConverter.ToSingle(data, offset + (1*4)),
                        System.BitConverter.ToSingle(data, offset + (2*4)),
                        System.BitConverter.ToSingle(data, offset + (3*4)),
                        System.BitConverter.ToSingle(data, offset + (4*4)),
                        System.BitConverter.ToSingle(data, offset + (5*4)),
                        System.BitConverter.ToSingle(data, offset + (6*4)),
                        System.BitConverter.ToSingle(data, offset + (7*4)),
                        System.BitConverter.ToSingle(data, offset + (8*4)),
                        System.BitConverter.ToSingle(data, offset + (9*4)),
                        System.BitConverter.ToSingle(data, offset + (10*4)),
                        System.BitConverter.ToSingle(data, offset + (11*4)),
                        System.BitConverter.ToSingle(data, offset + (12*4)),
                        System.BitConverter.ToSingle(data, offset + (13*4)),
                        System.BitConverter.ToSingle(data, offset + (14*4)),
                        System.BitConverter.ToSingle(data, offset + (15*4))
                    });
                }
                return retVal.ToArray();
            }
//            set
//            {
//                for (int x = 0; x < value.Length; x++)
//                {
//                    byte[] tmpData = System.BitConverter.GetBytes(value[x]);
//                    for (int y = 0; y < 4; y++)
//                    {
//                        value[(x * 4) + y] = tmpData[y];
//                    }
//                }
//            }
        }

        public Mat3DField(byte[] _data) : base(FIELD_TYPE.DATA_MAT3D, _data) { }

        public override string ToString()
        {
            //return "DATA_MAT3D: {" +
            //    "{" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "," + value4.ToString("G") + "}," +
            //    "{" + value5.ToString("G") + "," + value6.ToString("G") + "," + value7.ToString("G") + "," + value8.ToString("G") + "}," +
            //    "{" + value9.ToString("G") + "," + value10.ToString("G") + "," + value11.ToString("G") + "," + value12.ToString("G") + "}," +
            //    "{" + value13.ToString("G") + "," + value14.ToString("G") + "," + value15.ToString("G") + "," + value16.ToString("G") + "}" +
            //    "}";

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_MAT3D:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++)
            {
                retVal.Append(x > 0 ? ", " : "");
                retVal.Append("{{");
                retVal.Append(Value[x][0]);
                retVal.Append(",");
                retVal.Append(Value[x][1]);
                retVal.Append(",");
                retVal.Append(Value[x][2]);
                retVal.Append(",");
                retVal.Append(Value[x][3]);
                retVal.Append("},{");
                retVal.Append(Value[x][4]);
                retVal.Append(",");
                retVal.Append(Value[x][5]);
                retVal.Append(",");
                retVal.Append(Value[x][6]);
                retVal.Append(",");
                retVal.Append(Value[x][7]);
                retVal.Append("},{");
                retVal.Append(Value[x][8]);
                retVal.Append(",");
                retVal.Append(Value[x][9]);
                retVal.Append(",");
                retVal.Append(Value[x][10]);
                retVal.Append(",");
                retVal.Append(Value[x][11]);
                retVal.Append("},{");
                retVal.Append(Value[x][12]);
                retVal.Append(",");
                retVal.Append(Value[x][13]);
                retVal.Append(",");
                retVal.Append(Value[x][14]);
                retVal.Append(",");
                retVal.Append(Value[x][15]);
                retVal.Append("}}");
            }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }

    public class QuatField : Field
    {
        public float[][] Value
        {
//            get
//            {
//                return new float[] {
//                    System.BitConverter.ToSingle(data, 0*4),
//                    System.BitConverter.ToSingle(data, 1*4),
//                    System.BitConverter.ToSingle(data, 2*4),
//                    System.BitConverter.ToSingle(data, 3*4)
//                };
//            }
            get
            {
                List<float[]> retVal = new List<float[]>();
                for (int offset = 0; offset < data.Length; offset += (sizeof(float) * 4))
                {
                    //retVal.Add(System.BitConverter.ToInt16(data, offset));
                    retVal.Add(new float[] {
                        System.BitConverter.ToSingle(data, offset + (0 * 4)),
                        System.BitConverter.ToSingle(data, offset + (1 * 4)),
                        System.BitConverter.ToSingle(data, offset + (2 * 4)),
                        System.BitConverter.ToSingle(data, offset + (3 * 4))
                    });
                }
                return retVal.ToArray();
            }
//            set
//            {
//                for (int x = 0; x < value.Length; x++)
//                {
//                    byte[] tmpData = System.BitConverter.GetBytes(value[x]);
//                    for (int y = 0; y < 4; y++)
//                    {
//                        value[(x * 4) + y] = tmpData[y];
//                    }
//                }
//            }
        }

        public QuatField(byte[] _data) : base(FIELD_TYPE.DATA_QUAT, _data) { }

        public override string ToString()
        {
            //return "DATA_QUAT: {" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "," + value4.ToString("G") + "}";

            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_QUAT:\t");
            retVal.Append(data.Length);

            retVal.Append("\t[");
            for (int x = 0; x < Value.Length; x++)
            {
                retVal.Append(x > 0 ? ", " : "");
                retVal.Append("\t{");
                retVal.Append(Value[x][0]);
                retVal.Append(",");
                retVal.Append(Value[x][1]);
                retVal.Append(",");
                retVal.Append(Value[x][2]);
                retVal.Append(",");
                retVal.Append(Value[x][3]);
                retVal.Append("}");
            }
            retVal.Append("]\t");

            for (int x = 0; x < data.Length; x++)
            {
                retVal.Append(Convert.ToString(data[x], 16).PadLeft(2, '0'));
            }

            return retVal.ToString();
        }
    }


    /*public class StringField : Field
    {
        byte[] value;
        public StringField(byte[] value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_STRING: ");
            for (int x = 0; x < value.Length; x++)
            {
                retVal.Append(char.IsControl((char)value[x]) || value[x] == 0 ? "\\0x" + Convert.ToString(value[x], 16).PadLeft(2, '0') : "" + (char)value[x]);
            }
            return retVal.ToString();
        }

        public byte[] GetRawRef()
        {
            return null;
        }
    }
    */
}
