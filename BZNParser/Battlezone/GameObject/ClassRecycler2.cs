using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "recycler")]
    public class ClassRecycler2 : ClassFactory2
    {
        public UInt32 undefptr { get; set; }

        public ClassRecycler2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scrapTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scrapTimer/FLOAT");
            //scrapTimer = tok.GetSingle();

            base.LoadData(reader);
        }
    }
}
