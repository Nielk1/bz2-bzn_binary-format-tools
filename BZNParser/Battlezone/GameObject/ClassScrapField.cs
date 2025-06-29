﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrapfield")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrapfield")]
    [ObjectClass(BZNFormat.Battlezone2, "scrapfield")]
    public class ClassScrapFieldFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScrapField(preamble, classLabel);
            ClassScrapField.Hydrate(parent, reader, obj as ClassScrapField);
            return true;
        }
    }
    public class ClassScrapField : ClassBuilding
    {
        public ClassScrapField(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassScrapField? obj)
        {
            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
