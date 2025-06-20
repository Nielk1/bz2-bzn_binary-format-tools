using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "commtower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "commtower")]
    public class ClassCommTower1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassCommTower1(PrjID, isUser, classLabel);
            ClassCommTower1.Hydrate(reader, obj as ClassCommTower1);
            return true;
        }
    }
    public class ClassCommTower1 : ClassBuilding
    {
        public ClassCommTower1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassCommTower1? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
