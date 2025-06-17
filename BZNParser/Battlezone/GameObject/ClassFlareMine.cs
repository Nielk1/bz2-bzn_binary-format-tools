using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "flare")]
    [ObjectClass(BZNFormat.BattlezoneN64, "flare")]
    [ObjectClass(BZNFormat.Battlezone2, "flare")]
    public class ClassFlareMine : ClassMine
    {
        public ClassFlareMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //saveClass = tok.GetSingle();
                //shotTimer

                //tok = reader.ReadToken();
                //if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //saveClass = tok.GetSingle();
            }

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
    [ObjectClass(BZNFormat.Battlezone2, "tripmine")]
    public class ClassTripMine : ClassMine
    {
        public ClassTripMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
