﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "tripmine")]
    public class ClassTripMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTripMine(PrjID, isUser, classLabel);
            ClassTripMine.Hydrate(reader, obj as ClassTripMine);
            return true;
        }
    }
    public class ClassTripMine : ClassMine
    {
        public ClassTripMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassTripMine? obj)
        {
            ClassMine.Hydrate(reader, obj as ClassMine);
        }
    }
}
