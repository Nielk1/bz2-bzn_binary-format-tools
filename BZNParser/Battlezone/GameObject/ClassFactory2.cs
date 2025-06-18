using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "factory")]
    public class ClassFactory2 : ClassPoweredBuilding
    {
        public ClassFactory2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Version == 1100 || reader.Version == 1105)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("buildDoneTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildDoneTime/FLOAT");
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("buildTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildTime/FLOAT");
            }

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

            //...

            // if reader.SaveType != 0

            if (reader.SaveType == 0)
            {
                if (reader.Version >= 1135)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("navHandle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse navHandle/LONG");
                    //int navHandle = tok.GetInt32();
                }
            }

            base.LoadData(reader);
        }
    }
}
