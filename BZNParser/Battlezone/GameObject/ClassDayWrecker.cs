﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "daywrecker")]
    [ObjectClass(BZNFormat.BattlezoneN64, "daywrecker")]
    [ObjectClass(BZNFormat.Battlezone2, "daywrecker")]
    public class ClassDayWreckerFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassDayWrecker(preamble, classLabel);
            ClassDayWrecker.Hydrate(parent, reader, obj as ClassDayWrecker);
            return true;
        }
    }
    public class ClassDayWrecker : ClassPowerUp
    {
        public ClassDayWrecker(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassDayWrecker? obj)
        {
            ClassPowerUp.Hydrate(parent, reader, obj as ClassPowerUp);
        }
    }
}
