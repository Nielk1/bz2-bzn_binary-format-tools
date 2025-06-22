using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "plant")]
    public class ClassPlantFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassPlant(PrjID, isUser, classLabel);
            ClassPlant.Hydrate(parent, reader, obj as ClassPlant);
            return true;
        }
    }
    public class ClassPlant : ClassBuilding
    {
        public ClassPlant(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassPlant? obj)
        {
            IBZNToken tok;

            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
