using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassRocketFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRocket(PrjID, isUser, classLabel);
            ClassRocket.Hydrate(reader, obj as ClassRocket);
            return true;
        }
    }
    public class ClassRocket : ClassBullet
    {
        public ClassRocket(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassRocket? obj)
        {
            ClassBullet.Hydrate(reader, obj as ClassBullet);
        }
    }
}
