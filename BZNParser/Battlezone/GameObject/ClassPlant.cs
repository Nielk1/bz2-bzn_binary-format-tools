using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "plant")]
    public class ClassPlantFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassPlant(PrjID, isUser, classLabel);
            ClassPlant.Build(reader, obj as ClassPlant);
            return true;
        }
    }
    public class ClassPlant : ClassBuilding
    {
        public ClassPlant(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassPlant? obj)
        {
            IBZNToken tok;

            ClassBuilding.Build(reader, obj as ClassBuilding);
        }
    }
}
