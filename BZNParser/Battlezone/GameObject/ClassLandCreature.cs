using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "animal")]
    public class ClassLandCreatureFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassLandCreature(PrjID, isUser, classLabel);
            ClassLandCreature.Build(reader, obj as ClassLandCreature);
            return true;
        }
    }
    public class ClassLandCreature : ClassCraft
    {
        public ClassLandCreature(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassLandCreature? obj)
        {
            ClassCraft.Build(reader, obj as ClassCraft);
        }
    }
}
