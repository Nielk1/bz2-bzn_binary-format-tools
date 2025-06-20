using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "jammer")]
    public class ClassJammerTowerFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassJammerTower(PrjID, isUser, classLabel);
            ClassJammerTower.Build(reader, obj as ClassJammerTower);
            return true;
        }
    }
    public class ClassJammerTower : ClassPoweredBuilding
    {
        public ClassJammerTower(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassJammerTower? obj)
        {
            ClassPoweredBuilding.Build(reader, obj as ClassPoweredBuilding);
        }
    }
}
