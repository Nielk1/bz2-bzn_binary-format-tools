using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "powerplant")]
    [ObjectClass(BZNFormat.BattlezoneN64, "powerplant")]
    [ObjectClass(BZNFormat.Battlezone2, "powerplant")]
    [ObjectClass(BZNFormat.Battlezone2, "powerlung")]
    public class ClassPowerPlantFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassPowerPlant(PrjID, isUser, classLabel);
            ClassPowerPlant.Hydrate(reader, obj as ClassPowerPlant);
            return true;
        }
    }
    public class ClassPowerPlant : ClassBuilding
    {
        public ClassPowerPlant(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassPowerPlant? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
