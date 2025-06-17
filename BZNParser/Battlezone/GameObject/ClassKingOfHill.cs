using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "kingofhill")]
    public class ClassKingOfHill : ClassBuilding
    {
        public ClassKingOfHill(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scoreTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scoreTimer/FLOAT");
            float scoreTimer = tok.GetSingle();

            base.LoadData(reader);
        }
    }
}
