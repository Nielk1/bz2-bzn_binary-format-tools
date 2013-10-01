using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BinaryBZNFile
{
    public class BinaryBZN
    {
        public string VersionField;
        public string Version;
        public string SaveTypeField;
        public string SaveType;
        public string BinarySaveField;
        public string BinarySave;
        public List<Field> fields;

        public BinaryBZN(Stream filestream)
        {
            VersionField = "";
            Version = "";
            SaveTypeField = "";
            SaveType = "";
            BinarySaveField = "";
            BinarySave = "";
            fields = new List<Field>();

            // this reading code is awful btw
            byte[] readByte = new byte[1];
            bool saw0D = false;
            int readingLine = 0;
            List<byte> tmpBytes = new List<byte>();
            while (filestream.Read(readByte, 0, 1) > 0)
            {
                byte byteX = readByte[0];
                if (saw0D)
                {
                    if (byteX == 0x0A)
                    {
                        readingLine++;
                        //if (readingLine > 5)
                        //{
                        //    break;
                        //}
                        //else
                        //{
                            continue;
                        //}
                    }else if (byteX == 0x0D)
                    {
                        //the fook?
                    }
                    saw0D = false;
                }
                if (byteX == 0x0D)
                {
                    saw0D = true;
                }
                else
                {
                    saw0D = false;
                    switch (readingLine)
                    {
                        case 0:
                            VersionField += (char)byteX;
                            break;
                        case 1:
                            Version += (char)byteX;
                            break;
                        case 2:
                            SaveTypeField += (char)byteX;
                            break;
                        case 3:
                            SaveType += (char)byteX;
                            break;
                        case 4:
                            BinarySaveField += (char)byteX;
                            break;
                        case 5:
                            BinarySave += (char)byteX;
                            break;
                        default:
                            tmpBytes.Add(byteX);
                            break;
                    }
                }
            }
            byte[] dataBytes = tmpBytes.ToArray();

            if (!BinarySave.ToLowerInvariant().Equals("true"))
            {
                throw new Exception("Binary flag not \"true\".");
            }

            for (int x = 0; x < dataBytes.Length;)
            {
                ushort Size = (ushort)((dataBytes[x + 2] << 8) | dataBytes[x + 1]);
                switch (dataBytes[x])
                {
                    case 0: // DATA_VOID
                        fields.Add(new UnknownField("DATA_VOID", Size));
                        break;
                    case 1: // DATA_BOOL
                        fields.Add(new BoolField(dataBytes[x + 3] != 0x00));
                        break;
                    case 2: // DATA_CHAR
                        fields.Add(new CharField(dataBytes.Skip(x + 3).Take(Size).ToArray()));
                        break;
                    case 3: // DATA_SHORT
                        fields.Add(new ShortField((short)((dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        break;
                    case 4: // DATA_LONG
                        fields.Add(new LongField((long)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        break;
                    case 5: // DATA_FLOAT
                        //fields.Add(new FloatField((float)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        fields.Add(new FloatField(System.BitConverter.ToSingle(dataBytes, x + 3)));
                        break;
                    case 6: // DATA_DOUBLE
                        fields.Add(new DoubleField((double)(
                            (dataBytes[x + 10] << 64) |
                            (dataBytes[x + 9] << 56) |
                            (dataBytes[x + 8] << 48) |
                            (dataBytes[x + 7] << 40) |
                            (dataBytes[x + 6] << 32) |
                            (dataBytes[x + 5] << 16) |
                            (dataBytes[x + 4] << 8) |
                            dataBytes[x + 3])));
                        break;
                    case 7: // DATA_ID
                        fields.Add(new UnknownField("DATA_ID", Size));
                        break;
                    case 8: // DATA_PTR
                        fields.Add(new PtrField((long)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3])));
                        break;
                    case 9: // DATA_VEC3D
                        //fields.Add(new Vec2DField((float)((dataBytes[x + 6] << 24) | (dataBytes[x + 5] << 16) | (dataBytes[x + 4] << 8) | dataBytes[x + 3]),
                        //                          (float)((dataBytes[x + 10] << 56) | (dataBytes[x + 9] << 48) | (dataBytes[x + 8] << 40) | (dataBytes[x + 7] << 32))));
                        fields.Add(new Vec2DField(System.BitConverter.ToSingle(dataBytes, x + 3),
                                                  System.BitConverter.ToSingle(dataBytes, x + 7)));
                        break;
                    case 10: // DATA_VEC2D
                        //fields.Add(new Vec3DField((float)((dataBytes[x + 6]  << 24) | (dataBytes[x + 5]  << 16) | (dataBytes[x + 4]  << 8)  |  dataBytes[x + 3]),
                        //                          (float)((dataBytes[x + 10] << 56) | (dataBytes[x + 9]  << 48) | (dataBytes[x + 8]  << 40) | (dataBytes[x + 7]  << 32)),
                        //                          (float)((dataBytes[x + 13] << 88) | (dataBytes[x + 13] << 80) | (dataBytes[x + 12] << 72) | (dataBytes[x + 11] << 64))));
                        fields.Add(new Vec3DField(System.BitConverter.ToSingle(dataBytes, x + 3),
                                                  System.BitConverter.ToSingle(dataBytes, x + 7),
                                                  System.BitConverter.ToSingle(dataBytes, x + 11)));
                        break;
                    case 11: // DATA_MAT3DOLD
                        fields.Add(new UnknownField("DATA_MAT3DOLD", Size));
                        break;
                    case 12: // DATA_MAT3D
                        //fields.Add(new Mat3DField((float)((dataBytes[x +  6] << (8 *  3)) | (dataBytes[x +  5] << (8 *  2)) | (dataBytes[x +  4] <<  8      ) |  dataBytes[x +  3]),
                        //                          (float)((dataBytes[x + 10] << (8 *  7)) | (dataBytes[x +  9] << (8 *  6)) | (dataBytes[x +  8] << (8 *  5)) | (dataBytes[x +  7] << (8 * 4))),
                        //                          (float)((dataBytes[x + 14] << (8 * 11)) | (dataBytes[x + 13] << (8 * 10)) | (dataBytes[x + 12] << (8 *  9)) | (dataBytes[x + 11] << (8 * 8))),
                        //                          (float)((dataBytes[x + 18] << (8 * 15)) | (dataBytes[x + 17] << (8 * 14)) | (dataBytes[x + 16] << (8 * 13)) | (dataBytes[x + 15] << (8 * 12))),
                        //                          (float)((dataBytes[x + 22] << (8 * 19)) | (dataBytes[x + 21] << (8 * 18)) | (dataBytes[x + 20] << (8 * 17)) | (dataBytes[x + 19] << (8 * 16))),
                        //                          (float)((dataBytes[x + 26] << (8 * 23)) | (dataBytes[x + 25] << (8 * 22)) | (dataBytes[x + 24] << (8 * 21)) | (dataBytes[x + 23] << (8 * 20))),
                        //                          (float)((dataBytes[x + 30] << (8 * 27)) | (dataBytes[x + 29] << (8 * 26)) | (dataBytes[x + 28] << (8 * 25)) | (dataBytes[x + 27] << (8 * 24))),
                        //                          (float)((dataBytes[x + 34] << (8 * 31)) | (dataBytes[x + 33] << (8 * 30)) | (dataBytes[x + 32] << (8 * 29)) | (dataBytes[x + 31] << (8 * 28))),
                        //                          (float)((dataBytes[x + 38] << (8 * 38)) | (dataBytes[x + 37] << (8 * 37)) | (dataBytes[x + 36] << (8 * 33)) | (dataBytes[x + 35] << (8 * 32))),
                        //                          (float)((dataBytes[x + 42] << (8 * 42)) | (dataBytes[x + 41] << (8 * 41)) | (dataBytes[x + 40] << (8 * 37)) | (dataBytes[x + 39] << (8 * 36))),
                        //                          (float)((dataBytes[x + 46] << (8 * 46)) | (dataBytes[x + 45] << (8 * 45)) | (dataBytes[x + 44] << (8 * 41)) | (dataBytes[x + 33] << (8 * 40))),
                        //                          (float)((dataBytes[x + 50] << (8 * 50)) | (dataBytes[x + 49] << (8 * 49)) | (dataBytes[x + 48] << (8 * 45)) | (dataBytes[x + 37] << (8 * 44))),
                        //                          (float)((dataBytes[x + 54] << (8 * 54)) | (dataBytes[x + 53] << (8 * 53)) | (dataBytes[x + 52] << (8 * 49)) | (dataBytes[x + 51] << (8 * 48))),
                        //                          (float)((dataBytes[x + 58] << (8 * 58)) | (dataBytes[x + 57] << (8 * 54)) | (dataBytes[x + 56] << (8 * 53)) | (dataBytes[x + 55] << (8 * 52))),
                        //                          (float)((dataBytes[x + 62] << (8 * 62)) | (dataBytes[x + 61] << (8 * 58)) | (dataBytes[x + 60] << (8 * 57)) | (dataBytes[x + 59] << (8 * 56))),
                        //                          (float)((dataBytes[x + 66] << (8 * 63)) | (dataBytes[x + 65] << (8 * 62)) | (dataBytes[x + 64] << (8 * 61)) | (dataBytes[x + 63] << (8 * 60)))));
                        fields.Add(new Mat3DField(System.BitConverter.ToSingle(dataBytes, x + 3),
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
                                                  System.BitConverter.ToSingle(dataBytes, x + 63)));
                        break;
                    case 13: // DATA_STRING
                        fields.Add(new StringField(dataBytes.Skip(x + 3).Take(Size).ToArray()));
                        break;
                    case 14: // DATA_QUAT
                        fields.Add(new QuatField((float)((dataBytes[x +  6] << (8 *  3)) | (dataBytes[x +  5] << (8 *  2)) | (dataBytes[x +  4] <<  8)       |  dataBytes[x + 3]),
                                                 (float)((dataBytes[x + 10] << (8 *  7)) | (dataBytes[x +  9] << (8 *  6)) | (dataBytes[x +  8] << (8 *  5)) | (dataBytes[x + 7] << (8 * 4))),
                                                 (float)((dataBytes[x + 14] << (8 * 11)) | (dataBytes[x + 13] << (8 * 10)) | (dataBytes[x + 12] << (8 *  9)) | (dataBytes[x + 11] << (8 * 8))),
                                                 (float)((dataBytes[x + 18] << (8 * 15)) | (dataBytes[x + 17] << (8 * 14)) | (dataBytes[x + 16] << (8 * 13)) | (dataBytes[x + 15] << (8 * 12)))));
                        break;
                }
                x += 3 + Size;
            }
        }
    }

    public interface Field
    {

    }

    public class UnknownField : Field
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
    }

    public class BoolField : Field
    {
        bool value;
        public BoolField(bool value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "DATA_BOOL: " + value;
        }
    }

    public class CharField : Field
    {
        byte[] value;
        public CharField(byte[] value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("DATA_CHAR: ");
            if (value.Length == 1)
            {
                retVal.Append(char.IsControl((char)value[0]) || value[0] == 0 ? "\\0x" + Convert.ToString(value[0], 16).PadLeft(2, '0') : "" + (char)value[0]);
                retVal.Append("\t0x" + Convert.ToString(value[0], 16).PadLeft(2, '0'));
                retVal.Append("\t" + Convert.ToString(value[0], 10));
            }
            else
            {
                for (int x = 0; x < value.Length; x++)
                {
                    retVal.Append(char.IsControl((char)value[x]) || value[x] == 0 ? "\\0x" + Convert.ToString(value[x], 16).PadLeft(2, '0') : "" + (char)value[x]);
                }
                retVal.Append("\t");
                for (int x = 0; x < value.Length; x++)
                {
                    retVal.Append(Convert.ToString(value[x], 16).PadLeft(2, '0'));
                }
            }
            return retVal.ToString();
        }
    }

    public class ShortField : Field
    {
        short value;
        public ShortField(short value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "DATA_SHORT: " + value + "\t" + Convert.ToString(value, 16) + "h";
        }
    }

    public class LongField : Field
    {
        long value;
        public LongField(long value)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return "DATA_LONG: " + value + "\t" + Convert.ToString(value, 16) + "h";
        }

    }

    public class FloatField : Field
    {
        float value;
        public FloatField(float value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "DATA_FLOAT: " + value.ToString("G");
        }
    }

    public class DoubleField : Field
    {
        double value;
        public DoubleField(double value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "DATA_DOUBLE: " + value.ToString("G");
        }
    }

    public class PtrField : Field
    {
        long value;
        public PtrField(long value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "DATA_PTR: 0x" + Convert.ToString(value, 16).PadLeft(8, '0');
        }
    }

    public class Vec2DField : Field
    {
        float value1;
        float value2;
        public Vec2DField(float value1, float value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public override string ToString()
        {
            return "DATA_VEC2D: {" + value1.ToString("G") + "," + value2.ToString("G") + "}";
        }
    }

    public class Vec3DField : Field
    {
        float value1;
        float value2;
        float value3;
        public Vec3DField(float value1, float value2, float value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public override string ToString()
        {
            return "DATA_VEC3D: {" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "}";
        }
    }

    public class Mat3DField : Field
    {
        float value1;
        float value2;
        float value3;
        float value4;
        float value5;
        float value6;
        float value7;
        float value8;
        float value9;
        float value10;
        float value11;
        float value12;
        float value13;
        float value14;
        float value15;
        float value16;
        public Mat3DField(float value1, float value2, float value3, float value4,
                          float value5, float value6, float value7, float value8,
                          float value9, float value10, float value11, float value12,
                          float value13, float value14, float value15, float value16)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;
            this.value6 = value6;
            this.value7 = value7;
            this.value8 = value8;
            this.value9 = value9;
            this.value10 = value10;
            this.value11 = value11;
            this.value12 = value12;
            this.value13 = value13;
            this.value14 = value14;
            this.value15 = value15;
            this.value16 = value16;
        }

        public override string ToString()
        {
            return "DATA_MAT3D: {" +
                "{" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "," + value4.ToString("G") + "}," +
                "{" + value5.ToString("G") + "," + value6.ToString("G") + "," + value7.ToString("G") + "," + value8.ToString("G") + "}," +
                "{" + value9.ToString("G") + "," + value10.ToString("G") + "," + value11.ToString("G") + "," + value12.ToString("G") + "}," +
                "{" + value13.ToString("G") + "," + value14.ToString("G") + "," + value15.ToString("G") + "," + value16.ToString("G") + "}" +
                "}";
        }
    }

    public class QuatField : Field
    {
        float value1;
        float value2;
        float value3;
        float value4;
        public QuatField(float value1, float value2, float value3, float value4)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
        }

        public override string ToString()
        {
            return "DATA_QUAT: {" + value1.ToString("G") + "," + value2.ToString("G") + "," + value3.ToString("G") + "," + value4.ToString("G") + "}";
        }
    }

    public class StringField : Field
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
    }
}
