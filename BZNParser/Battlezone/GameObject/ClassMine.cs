using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    // Done BZCC

    public class ClassMine : ClassBuilding
    {
        public ClassMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Format == BZNFormat.Battlezone2)
            {
                IBZNToken tok = reader.ReadToken();
                if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //saveClass = tok.GetSingle();
                //lifeTimer

                //if (reader.Version > 1123)
                //{
                //    IBZNToken tok;
                //
                //    long pos = reader.BaseStream.Position;
                //
                //    tok = reader.ReadToken();
                //    if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT))
                //    {
                //        reader.BaseStream.Position = pos;
                //    }
                //    else
                //    {
                //        if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //        float undeffloat = (int)tok.GetSingle();
                //    }
                //}
                //if (reader.Version >= 1180 && reader.Version != 1192) // unless it needs to be over 1192, do check
                //{
                //    IBZNToken tok;
                //
                //    tok = reader.ReadToken();
                //    if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //    float undeffloat = (int)tok.GetSingle();
                //}
            }

            base.LoadData(reader);
        }
    }
}
