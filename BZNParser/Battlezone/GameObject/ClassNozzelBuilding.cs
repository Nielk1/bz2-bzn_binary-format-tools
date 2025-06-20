using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "cnozzle")]
    public class ClassNozzelBuildingFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassNozzelBuilding(PrjID, isUser, classLabel);
            ClassNozzelBuilding.Build(reader, obj as ClassNozzelBuilding);
            return true;
        }
    }
    public class ClassNozzelBuilding: ClassBuilding
    {
        public ClassNozzelBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassNozzelBuilding? obj)
        {
            ClassBuilding.Build(reader, obj as ClassBuilding);
        }
    }
}
