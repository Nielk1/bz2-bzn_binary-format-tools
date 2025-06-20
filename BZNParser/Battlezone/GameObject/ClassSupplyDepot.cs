using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "supplydepot")]
    [ObjectClass(BZNFormat.BattlezoneN64, "supplydepot")]
    public class ClassSupplyDepotFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSupplyDepot(PrjID, isUser, classLabel);
            ClassSupplyDepot.Hydrate(reader, obj as ClassSupplyDepot);
            return true;
        }
    }
    public class ClassSupplyDepot : ClassBuilding
    {
        public ClassSupplyDepot(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassSupplyDepot? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
