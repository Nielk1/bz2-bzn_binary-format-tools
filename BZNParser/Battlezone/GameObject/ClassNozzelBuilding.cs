using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "cnozzle")]
    public class ClassNozzelBuildingFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassNozzelBuilding(PrjID, isUser, classLabel);
            ClassNozzelBuilding.Hydrate(parent, reader, obj as ClassNozzelBuilding);
            return true;
        }
    }
    public class ClassNozzelBuilding: ClassBuilding
    {
        public ClassNozzelBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassNozzelBuilding? obj)
        {
            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
