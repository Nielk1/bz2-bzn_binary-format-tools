using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassRocketFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRocket(PrjID, isUser, classLabel);
            ClassRocket.Build(reader, obj as ClassRocket);
            return true;
        }
    }
    public class ClassRocket : ClassBullet
    {
        public ClassRocket(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassRocket? obj)
        {
            ClassBullet.Build(reader, obj as ClassBullet);
        }
    }
}
