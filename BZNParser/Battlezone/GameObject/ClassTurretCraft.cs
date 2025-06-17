using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "turret")]
    [ObjectClass(BZNFormat.BattlezoneN64, "turret")]
    [ObjectClass(BZNFormat.Battlezone2, "turret")]
    public class ClassTurretCraft : ClassCraft
    {
        public ClassTurretCraft(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1068)
                {
                    if (reader.Version >= 1072)
                    {
                        // we don't know how many taps there are without the ODF, so just try to read forever
                        for (; ; )
                        {
                            long pos = reader.BaseStream.Position;
                            tok = reader.ReadToken();
                            if (tok.Validate("powerHandle", BinaryFieldType.DATA_LONG))
                            {
                                UInt32 powerHandle = tok.GetUInt32();
                            }
                            else
                            {
                                reader.BaseStream.Position = pos;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // we don't know how many taps there are without the ODF, so just try to read forever
                        long pos = reader.BaseStream.Position;
                        tok = reader.ReadToken();
                        if (tok.Validate("powerHandle", BinaryFieldType.DATA_LONG))
                        {
                            UInt32 powerHandle = tok.GetUInt32();
                            try
                            {
                                UInt32 powerHandle2 = tok.GetUInt32(1);
                            }
                            catch { }
                        }
                        else
                        {
                            reader.BaseStream.Position = pos;
                        }
                    }
                }

                // reader.SaveType != 0
                /*if (a2[2].vftable)
                {
                    (a2->vftable->out_bool)(a2, this + 2376, 1, "terminalOn");
                    (a2->vftable->read_long)(a2, this + 2380, 4, "terminalUser");
                    (a2->vftable->read_long)(a2, this + 2332, 4, "originalTeam");
                    (a2->vftable->read_long)(a2, this + 2336, 4, "originalGroup");
                    v5 = 0;
                    v8 = 0;
                    if (*(this + 329) > 0)
                    {
                        v6 = (this + 2340);
                        do
                        {
                            if (*v6)
                            {
                                TurretControl::Save(*v6, a2);
                                v5 = v8;
                            }
                            v5 = (v5 + 1);
                            ++v6;
                            v8 = v5;
                        }
                        while (v5 < *(this + 329));
                        v4 = v7;
                    }
                }*/

                bool m_AlignsToObject = false;

                if (reader.Version >= 1158)
                {
                    // saveClass must have a CHAR token as its first token if it's in binary mode, meaning the above loop consuming all LONGs is fine
                    // if the version was lower we might have had a LONG conflict
                    string saveClass = reader.ReadGameObjectClass_BZ2("saveClass");

                    //if (*(this + 376))
                    if (!string.IsNullOrEmpty(saveClass))
                    {
                        long pos = reader.BaseStream.Position;
                        tok = reader.ReadToken();
                        if (tok.Validate("saveMatrix", BinaryFieldType.DATA_MAT3D))
                        {
                            Matrix saveMatrix = tok.GetMatrix();
                        }
                        else
                        {
                            //throw new Exception("Failed to parse saveMatrix/MAT3D"); // type not confirmed
                            reader.BaseStream.Position = pos;
                            m_AlignsToObject = true;
                        }

                        tok = reader.ReadToken();
                        if (!tok.Validate("saveTeam", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse saveTeam/LONG");
                        //tok.GetUInt32();

                        tok = reader.ReadToken();
                        if (!tok.Validate("saveSeqno", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse saveSeqno/LONG");
                        //seqno = tok.GetUInt32H();

                        //tok = reader.ReadToken();
                        //if (!tok.Validate("saveLabel", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse saveLabel/CHAR");
                        //tok.GetString();
                        string saveLabel = reader.ReadBZ2InputString("saveLabel");

                        //tok = reader.ReadToken();
                        //if (!tok.Validate("saveName", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse saveName/CHAR");
                        //tok.GetString();
                        string saveName = reader.ReadBZ2InputString("saveName");
                    }
                }

                if (reader.Version >= 1193)
                {
                    // because the version needs of this are even higher than that of the above we know the above will have to have run if this will run
                    // so we know the powerHandle loop is safe since it will trip into a CHAR if it overruns due to the above.
                    tok = reader.ReadToken();
                    if (!tok.Validate("scriptPowerOverride", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse scriptPowerOverride/LONG");
                    Int32 autoTarget = tok.GetInt32();
                }

                base.LoadData(reader);

                if (m_AlignsToObject)
                {
                    // saveMatrix = GetSimObjectMatrix();
                }

                return;
            }

            base.LoadData(reader);
            return;
        }
    }
}
