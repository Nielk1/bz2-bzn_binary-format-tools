﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassGrenadeFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassGrenade(PrjID, isUser, classLabel);
            ClassGrenade.Hydrate(reader, obj as ClassGrenade);
            return true;
        }
    }
    public class ClassGrenade : ClassRocket
    {
        public ClassGrenade(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassGrenade? obj)
        {
            ClassRocket.Hydrate(reader, obj as ClassRocket);
        }
    }
}
