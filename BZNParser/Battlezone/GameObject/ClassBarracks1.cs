using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "barracks")]
    [ObjectClass(BZNFormat.BattlezoneN64, "barracks")]
    public class ClassBarracks1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBarracks1(PrjID, isUser, classLabel);
            ClassBarracks1.Build(reader, obj as ClassBarracks1);
            return true;
        }
    }
    public class ClassBarracks1 : ClassBuilding
    {
        public ClassBarracks1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Build(BZNStreamReader reader, ClassBarracks1? obj)
        {
            ClassBuilding.Build(reader, obj as ClassBuilding);
        }
    }
}
