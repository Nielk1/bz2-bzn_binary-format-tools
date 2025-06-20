using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "boid")]
    public class ClassBoidFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBoid(PrjID, isUser, classLabel);
            ClassBoid.Hydrate(reader, obj as ClassBoid);
            return true;
        }
    }
    public class ClassBoid : ClassCraft
    {
        public ClassBoid(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNStreamReader reader, ClassBoid? obj)
        {
            ClassCraft.Hydrate(reader, obj as ClassCraft);
        }
    }
}
