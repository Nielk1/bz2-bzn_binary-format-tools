using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spraybomb")]
    [ObjectClass(BZNFormat.BattlezoneN64, "spraybomb")]
    [ObjectClass(BZNFormat.Battlezone2, "spraybomb")]
    public class ClassSprayBuildingFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSprayBuilding(PrjID, isUser, classLabel);
            ClassSprayBuilding.Build(reader, obj as ClassSprayBuilding);
            return true;
        }
    }
    public class ClassSprayBuilding : ClassBuilding
    {
        public ClassSprayBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassSprayBuilding? obj)
        {
            ClassBuilding.Build(reader, obj as ClassBuilding);
        }
    }
}
