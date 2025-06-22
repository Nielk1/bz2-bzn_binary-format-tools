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

    [ObjectClass(BZNFormat.Battlezone, "artifact")]
    [ObjectClass(BZNFormat.BattlezoneN64, "artifact")]
    [ObjectClass(BZNFormat.Battlezone2, "artifact")]
    public class ClassBuildingFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBuilding(PrjID, isUser, classLabel);
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
            return true;
        }
    }
    public class ClassBuilding : ClassGameObject
    {
        public ClassBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassBuilding? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                bool m_AlignsToObject = false;
                string saveClass = null;

                if (reader.Version >= 1147)
                {
                    if (reader.Version == 1147 || reader.Version == 1148 || reader.Version == 1149 || reader.Version == 1151 || reader.Version == 1154)
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
                            reader.Bookmark.Push();
                            tok = reader.ReadToken();
                            if (tok.Validate("saveMatrix", BinaryFieldType.DATA_MAT3D))
                            {
                                reader.Bookmark.Discard();
                                Matrix saveMatrix = tok.GetMatrix();
                            }
                            else
                            {
                                //throw new Exception("Failed to parse saveMatrix/MAT3D"); // type not confirmed
                                reader.Bookmark.Pop();
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
                reader.Bookmark.Push();
                tok = reader.ReadToken();
                loadAsDummy = tok.Validate("name", BinaryFieldType.DATA_CHAR);
                reader.Bookmark.Pop();
                if (loadAsDummy)
                {
                    if (obj != null) obj.name = reader.ReadSizedString_BZ2_1145("name", 32);
                    return;
                }

                ClassGameObject.Hydrate(reader, obj as ClassGameObject);

                if (!string.IsNullOrEmpty(saveClass) && (reader.Version < 1148 || m_AlignsToObject))
                {

                }
                return;
            }

            ClassGameObject.Hydrate(reader, obj as ClassGameObject);
        }
    }
}
