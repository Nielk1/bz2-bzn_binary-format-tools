using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassCraft : ClassGameObject
    {
        public Int32 abandoned { get; set; }

        public ClassCraft(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Version == 1041 && ClassLabel == "turret") // version is special case for bz2001.bzn
            {
                // add format to this if
                base.LoadData(reader);
                return;
            }

            if (reader.Format == BZNFormat.Battlezone2 && ClassLabel == "turret" &&
                 (reader.Version == 1105 || reader.Version == 1108))
            {
                base.LoadData(reader);
                return;
            }

            IBZNToken tok;

            //if (reader.Format == BZNFormat.BattlezoneN64 || ((reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.Battlezone2) && reader.Version > 1022))
            //if (reader.Format == BZNFormat.BattlezoneN64 || ((reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.Battlezone2) && reader.Version >= 1037))
            if (reader.Format == BZNFormat.BattlezoneN64 || ((reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.Battlezone2) && reader.Version >= 1034))
            {
                tok = reader.ReadToken();
                if (!tok.Validate("abandoned", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse abandoned/LONG");
                abandoned = tok.GetInt32();
            }
            if (reader.Format == BZNFormat.Battlezone && reader.Version <= 1022)
            {
                tok = reader.ReadToken();//setAltitude [1] =
                //1
                tok = reader.ReadToken();//accelDragStop [1] =
                //3.5
                tok = reader.ReadToken();//accelDragFull [1] =
                //1
                tok = reader.ReadToken();//alphaTrack [1] =
                //20
                tok = reader.ReadToken();//alphaDamp [1] =
                //5
                tok = reader.ReadToken();//pitchPitch [1] =
                //0.25
                tok = reader.ReadToken();//pitchThrust [1] =
                //0.1
                tok = reader.ReadToken();//rollStrafe [1] =
                //0.1
                tok = reader.ReadToken();//rollSteer [1] =
                //0.1
                tok = reader.ReadToken();//velocForward [1] =
                //20
                tok = reader.ReadToken();//velocReverse [1] =
                //15
                tok = reader.ReadToken();//velocStrafe [1] =
                //20
                tok = reader.ReadToken();//accelThrust [1] =
                //20
                tok = reader.ReadToken();//accelBrake [1] =
                //75
                tok = reader.ReadToken();//omegaSpin [1] =
                //4
                tok = reader.ReadToken();//omegaTurn [1] =
                //1.5
                tok = reader.ReadToken();//alphaSteer [1] =
                //5
                tok = reader.ReadToken();//accelJump [1] =
                //20
                tok = reader.ReadToken();//thrustRatio [1] =
                //1
                tok = reader.ReadToken();//throttle [1] =
                //0
                tok = reader.ReadToken();//airBorne [1] =
                //5.96046e-008
            }

            //if (reader.Format == BZNFormat.Battlezone && (reader.Version >= 1032 && reader.Version <= 1033))
            if (reader.Format == BZNFormat.Battlezone && (reader.Version >= 1030 && reader.Version <= 1033))
            {
                tok = reader.ReadToken();
                if (!tok.Validate("????", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse ????/LONG");
                uint unknown = (uint)tok.GetUInt32H();
            }

            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2011)
            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2004)
            if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2003)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("cloakState", BinaryFieldType.DATA_VOID))
                    throw new Exception("Failed to parse cloakState/VOID");
                UInt32 cloakState = (uint)tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("cloakTransBeginTime", BinaryFieldType.DATA_FLOAT))
                    throw new Exception("Failed to parse cloakTransBeginTime/FLOAT");
                float cloakTransBeginTime = (uint)tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate("cloakTransEndTime", BinaryFieldType.DATA_FLOAT))
                    throw new Exception("Failed to parse cloakTransEndTime/FLOAT");
                float cloakTransEndTime = (uint)tok.GetSingle();
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1143)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("curAmmo", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse curAmmo/FLOAT");
                    curAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("maxAmmo", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse maxAmmo/FLOAT");
                    maxAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("addAmmo", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse addAmmo/FLOAT");
                    float addAmmo = (uint)tok.GetSingle();

                    if (reader.InBinary)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate(null, BinaryFieldType.DATA_CHAR))
                            throw new Exception("Failed to parse ?/CHAR");
                        byte curPilotLength = tok.GetUInt8();

                        if (curPilotLength > 0)
                        {
                            tok = reader.ReadToken();
                            if (!tok.Validate("curPilot", BinaryFieldType.DATA_CHAR))
                                throw new Exception("Failed to parse curPilot/CHAR");
                            curPilot = tok.GetString();
                        }
                    }
                    else
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("curPilot", BinaryFieldType.DATA_CHAR))
                        {
                            curPilot = null;
                            //throw new Exception("Failed to parse curPilot/CHAR");
                        }
                        else
                        {
                            curPilot = tok.GetString();
                        }
                    }

                    if (reader.Version >= 1195)
                    {
                        tok = reader.ReadToken();
                        if (reader.Version == 1196)
                        {
                            if (!tok.Validate("ejectRatio", BinaryFieldType.DATA_FLOAT))
                                throw new Exception("Failed to parse ejectRatio/FLOAT");
                        }
                        else
                        {
                            if (!tok.Validate("m_ejectRatio", BinaryFieldType.DATA_FLOAT))
                                throw new Exception("Failed to parse m_ejectRatio/FLOAT");
                            //m_ejectRatio = tok.GetSingle();
                        }
                    }
                }
            }

            base.LoadData(reader);
        }
    }
}
