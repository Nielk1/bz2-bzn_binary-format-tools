using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "seeker")]
    public class ClassSeekerFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSeeker(PrjID, isUser, classLabel);
            ClassSeeker.Hydrate(parent, reader, obj as ClassSeeker);
            return true;
        }
    }
    public class ClassSeeker : ClassMine
    {
        public ClassSeeker(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassSeeker? obj)
        {
            ClassMine.Hydrate(parent, reader, obj as ClassMine);
        }
    }
}
