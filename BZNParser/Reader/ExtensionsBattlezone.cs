using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BZNParser.Reader
{
    static class ExtensionsBattlezone
    {
        public static UInt32 ReadCompressedNumberFromBinary(this BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (tok.Validate(null, BinaryFieldType.DATA_LONG))
            {
                return tok.GetUInt32();
            }
            else if (tok.Validate(null, BinaryFieldType.DATA_SHORT))
            {
                return tok.GetUInt16();
            }
            else if (tok.Validate(null, BinaryFieldType.DATA_CHAR))
            {
                return tok.GetUInt8();
            }
            else
            {
                throw new Exception("Failed to parse LONG/SHORT/CHAR");
            }
        }
        public static string ReadBZ2InputString(this BZNStreamReader reader, string name)
        {
            IBZNToken tok;
            if (reader.InBinary)
            {
                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse ?/CHAR");
                UInt32 length = tok.GetUInt8();

                if (length > 0)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                    return tok.GetString();
                }
                return null;
            }

            tok = reader.ReadToken();
            if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
            return tok.GetString();
        }
        public static string ReadBZ2StringInSized(this BZNStreamReader reader, string name, int bufferSize)
        {
            IBZNToken tok;
            if (reader.InBinary)
            {
                UInt32 length = reader.ReadCompressedNumberFromBinary();

                if (length > 0)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                    return tok.GetString();
                }
                return null;
            }

            tok = reader.ReadToken();
            if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
            return tok.GetString();
        }
        
        public static string ReadGameObjectClass_BZ2(this BZNStreamReader reader, string name)
        {
            if (reader.Version < 1145)
            {
                return reader.ReadSizedString_BZ2_1145(name, 16);
            }
            else
            {
                if (reader.SaveType == 3)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    return reader.ReadBZ2InputString(name);
                }
            }
            return null;
        }

        /// <summary>
        /// Read a byte from the BZN which in ASCII mode might be stored as signed byte or raw bytes in the ASCII stream
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte ReadBytePossibleRawPossibleSigned_BZ2(this BZNStreamReader reader, string name)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse saveFlags/CHAR");
            if (reader.Version >= 1187)
            {
                return tok.GetUInt8();
            }
            else
            {
                return tok.GetRaw(0, 1)[0];
            }
        }

        /// <summary>
        /// Read a string with size, handles versions < 1145 as using bufferSize and >= 1145 as unbounded
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ReadSizedString_BZ2_1145(this BZNStreamReader reader, string name, int bufferSize)
        {
            if (reader.Format != BZNFormat.Battlezone2)
                throw new NotImplementedException();

            IBZNToken tok;

            if (reader.Version <= 1124) // <= 1124 <= 1112 <= 1108 <= 1105?  < 1103
            {
                tok = reader.ReadToken();
                if (!tok.Validate(name, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                return tok.GetString();
            }
            else if (reader.Version < 1145)
            {
                // bufferSize applies in this branch
                // in
                return ReadBZ2StringInSized(reader, name, bufferSize);
            }
            else
            {
                // inputstring
                return ReadBZ2InputString(reader, name);
            }
        }
        public static void GetAiCmdInfo(this BZNStreamReader reader)
        {
            UInt32 priority;
            UInt32 what;
            Int32 who;
            UInt32 where;
            UInt32 param;

            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("priority", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse priority/LONG");
            priority = tok.GetUInt32();

            tok = reader.ReadToken();
            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
                if (!tok.Validate("what", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse what/VOID");
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version < 1145)
                {
                    if (!tok.Validate("what", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse what/VOID");
                }
                else
                {
                    if (!tok.Validate("what", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse what/CHAR");
                }
                if (reader.InBinary)
                {
                    what = tok.GetUInt8();
                }
                else
                {
                    what = tok.GetUInt32H();
                }
            }

            tok = reader.ReadToken();
            if (!tok.Validate("who", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse who/LONG");
            who = tok.GetInt32();

            //if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                //if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
                //    if (!tok.Validate("where", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse where/VOID");
                //if (reader.Format == BZNFormat.Battlezone2)
                if (!tok.Validate("where", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse where/PTR");
                where = tok.GetUInt32H();

                tok = reader.ReadToken();
                if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2016)
                {
                    if (!tok.Validate("param", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse param/ID");
                    string tmp = tok.GetString();
                    if (tmp == string.Empty)
                    {
                        param = 0;
                    }
                    else
                    {
                        //param = tok.GetUInt32();
                        param = tok.GetRaw(0, 1)[0];
                    }
                }
                else
                {
                    if (!tok.Validate("param", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse param/LONG");
                    param = tok.GetUInt32();
                }
            }
        }

        public static Euler GetEuler(this BZNStreamReader reader)
        {
            if (reader.SaveType == 0)
            {
                if (reader.InBinary)
                {
                    IBZNToken tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_mass = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_mass_inv = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_v_mag = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_v_mag_inv = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_I = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                    float euler_k_i = tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's VEC3D");
                    Vector3D euler_v = tok.GetVector3D();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's VEC3D");
                    Vector3D euler_omega = tok.GetVector3D();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's VEC3D");
                    Vector3D euler_Accel = tok.GetVector3D();

                    Euler euler = new Euler()
                    {
                        mass = euler_mass,
                        mass_inv = euler_mass_inv,
                        v_mag = euler_v_mag,
                        v_mag_inv = euler_v_mag_inv,
                        I = euler_I,
                        k_i = euler_k_i,
                        v = euler_v,
                        omega = euler_omega,
                        Accel = euler_Accel
                    };

                    return euler;
                }
                else
                {
                    IBZNToken tok = reader.ReadToken();
                    if (!tok.Validate("euler")) throw new Exception("Failed to parse euler");
                    Euler euler = tok.GetEuler();

                    return euler;
                }
            }
            else if (reader.Version < 1145)
            {
                // text buffer
                throw new NotImplementedException("Version <1145 Euler Save");
            }
            else
            {
                // compressable data
                throw new NotImplementedException("Version <1145 Euler Save");
            }
        }
    }
}
