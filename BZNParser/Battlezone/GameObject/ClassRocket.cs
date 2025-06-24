﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassRocketFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRocket(preamble, classLabel);
            ClassRocket.Hydrate(parent, reader, obj as ClassRocket);
            return true;
        }
    }
    public class ClassRocket : ClassBullet
    {
        public ClassRocket(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassRocket? obj)
        {
            ClassBullet.Hydrate(parent, reader, obj as ClassBullet);
        }
    }
}
