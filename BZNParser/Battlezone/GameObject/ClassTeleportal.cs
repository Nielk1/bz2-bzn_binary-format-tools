using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "teleportal")]
    public class ClassTeleportalFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTeleportal(PrjID, isUser, classLabel);
            ClassTeleportal.Build(reader, obj as ClassTeleportal);
            return true;
        }
    }
    public class ClassTeleportal : ClassPoweredBuilding
    {
        public ClassTeleportal(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassTeleportal? obj)
        {
            ClassPoweredBuilding.Build(reader, obj as ClassPoweredBuilding);
        }
    }
}
