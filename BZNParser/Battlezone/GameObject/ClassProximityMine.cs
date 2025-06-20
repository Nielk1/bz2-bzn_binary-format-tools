using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "proximity")]
    [ObjectClass(BZNFormat.BattlezoneN64, "proximity")]
    [ObjectClass(BZNFormat.Battlezone2, "proximity")]
    public class ClassProximityMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassProximityMine(PrjID, isUser, classLabel);
            ClassProximityMine.Build(reader, obj as ClassProximityMine);
            return true;
        }
    }
    public class ClassProximityMine : ClassMine
    {
        public ClassProximityMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassProximityMine? obj)
        {
            ClassMine.Build(reader, obj as ClassMine);
        }
    }
}
