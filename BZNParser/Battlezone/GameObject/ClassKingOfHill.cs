using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "kingofhill")]
    public class ClassKingOfHillFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassKingOfHill(PrjID, isUser, classLabel);
            ClassKingOfHill.Hydrate(reader, obj as ClassKingOfHill);
            return true;
        }
    }
    public class ClassKingOfHill : ClassBuilding
    {
        public ClassKingOfHill(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassKingOfHill? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scoreTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scoreTimer/FLOAT");
            float scoreTimer = tok.GetSingle();

            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
