using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    // BZCC Done

    [ObjectClass(BZNFormat.Battlezone, "i76building")] // is not in code in the main area but appears to be valid?
    [ObjectClass(BZNFormat.Battlezone, "i76building2")] // is in code directly
    [ObjectClass(BZNFormat.Battlezone, "i76sign")]

    [ObjectClass(BZNFormat.BattlezoneN64, "i76building")] // is not in code in the main area but appears to be valid?
    [ObjectClass(BZNFormat.BattlezoneN64, "i76building2")] // is in code directly
    [ObjectClass(BZNFormat.BattlezoneN64, "i76sign")]

    [ObjectClass(BZNFormat.Battlezone2, "i76building")]
    [ObjectClass(BZNFormat.Battlezone2, "i76sign")]

    [ObjectClass(BZNFormat.Battlezone, "repairdepot")]
    [ObjectClass(BZNFormat.BattlezoneN64, "repairdepot")]
    public class ClassBuilding : ClassGameObject
    {
        public ClassBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                bool m_AlignsToObject = false;
                string saveClass = null;

                if (reader.Version >= 1147)
                {
                    if (reader.Version == 1154 || reader.Version == 1148 || reader.Version == 1149)
                    {
                        saveClass = reader.ReadGameObjectClass_BZ2("config");
                    }
                    else
                    {
                        saveClass = reader.ReadGameObjectClass_BZ2("saveClass");
                    }

                    if (!string.IsNullOrEmpty(saveClass))
                    {
                        if (reader.Version >= 1148)
                        {
                            long posX = reader.BaseStream.Position;
                            tok = reader.ReadToken();
                            if (tok.Validate("saveMatrix", BinaryFieldType.DATA_MAT3D))
                            {
                                Matrix saveMatrix = tok.GetMatrix();
                            }
                            else
                            {
                                //throw new Exception("Failed to parse saveMatrix/MAT3D"); // type not confirmed
                                reader.BaseStream.Position = posX;
                                m_AlignsToObject = true;
                            }
                        }

                        tok = reader.ReadToken();
                        if (!tok.Validate("saveTeam", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse saveTeam/LONG");
                        //tok.GetUInt32();

                        tok = reader.ReadToken();
                        if (!tok.Validate("saveSeqno", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse saveSeqno/LONG");
                        //seqno = tok.GetUInt32H();

                        string saveLabel = reader.ReadSizedString_BZ2_1145("saveLabel", 32);
                        string saveName = reader.ReadSizedString_BZ2_1145("saveName", 32);
                    }
                }

                bool loadAsDummy = false;
                long pos = reader.BaseStream.Position;
                tok = reader.ReadToken();
                loadAsDummy = tok.Validate("name", BinaryFieldType.DATA_CHAR);
                reader.BaseStream.Position = pos;
                if (loadAsDummy)
                {
                    name = reader.ReadSizedString_BZ2_1145("name", 32);
                    return;
                }

                base.LoadData(reader);

                if (!string.IsNullOrEmpty(saveClass) && (reader.Version < 1148 || m_AlignsToObject))
                {

                }
                return;
            }

            base.LoadData(reader);
        }
    }
}
