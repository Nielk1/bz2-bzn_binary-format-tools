using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassBulletFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBullet(PrjID, isUser, classLabel);
            ClassBullet.Hydrate(parent, reader, obj as ClassBullet);
            return true;
        }
    }
    public class ClassBullet : ClassOrdnance
    {
        public ClassBullet(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassBullet? obj)
        {
            ClassOrdnance.Hydrate(parent, reader, obj as ClassOrdnance);
        }
    }
}
