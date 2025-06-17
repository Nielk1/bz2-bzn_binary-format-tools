using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "silo")]
    public class ClassScrapSilo2 : ClassBuilding
    {
        public UInt32 undefptr { get; set; }

        public ClassScrapSilo2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scrapTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scrapTimer/FLOAT");
            //saveClass = tok.GetSingle();

            base.LoadData(reader);
        }
    }
}
