using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "torpedo")]
    public class ClassTorpedo2 : ClassGameObject
    {
        public ClassTorpedo2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("lifeTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse lifeTimer/FLOAT");
            float lifeTimer = tok.GetSingle();

            base.LoadData(reader);
        }
    }
}
