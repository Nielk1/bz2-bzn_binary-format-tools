using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "armory")]
    public class ClassArmory2 : ClassPoweredBuilding
    {
        public ClassArmory2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Version == 1041) // version is special case for bz2001.bzn
            {
                base.LoadData(reader);
                return;
            }

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("buildDoneTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildDoneTime/FLOAT");

            tok = reader.ReadToken();
            if (!tok.Validate("buildActive", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse buildActive/BOOL");

            tok = reader.ReadToken();
            if (!tok.Validate("buildCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse buildCount/LONG");
            int buildCount = tok.GetInt32();

            for (int i = 0; i < buildCount; i++)
            {
                //v5 = std::deque < GameObjectClass const *>::operator[] (v4);
                //ILoadSaveVisitor::out(a2, *v5, "buildItem");
                //++v4;
                string item = reader.ReadGameObjectClass_BZ2("buildItem");
            }
            //if (!a2[2].vftable
            //  || ((a2->vftable->out_bool)(a2, v3 + 2169, 1, "buildStall"),
            //      (a2->vftable->field_24)(a2, v3 + 2184, 12, "buildRally"),
            //      (a2->vftable->read_long)(a2, v3 + 2196, 4, "navHandle"),
            //      (a2->vftable->read_long)(a2, v3 + 2212, 4, "launchHandle"),
            //      (a2->vftable->field_24)(a2, v3 + 2200, 12, "launchTarget"),
            //      !a2[2].vftable))

            if (reader.SaveType == 0)
            {
                if (reader.Version >= 1135)
                {
                    //(a2->vftable->read_long)(a2, v3 + 2196, 4, "navHandle");
                    tok = reader.ReadToken();
                    if (!tok.Validate("navHandle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse navHandle/LONG");
                    //int navHandle = tok.GetInt32();
                }
            }

            base.LoadData(reader);
        }
    }
}
