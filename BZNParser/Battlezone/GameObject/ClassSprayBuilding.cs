﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spraybomb")]
    [ObjectClass(BZNFormat.BattlezoneN64, "spraybomb")]
    [ObjectClass(BZNFormat.Battlezone2, "spraybomb")]
    public class ClassSprayBuildingFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSprayBuilding(preamble, classLabel);
            ClassSprayBuilding.Hydrate(parent, reader, obj as ClassSprayBuilding);
            return true;
        }
    }
    public class ClassSprayBuilding : ClassBuilding
    {
        public ClassSprayBuilding(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassSprayBuilding? obj)
        {
            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
