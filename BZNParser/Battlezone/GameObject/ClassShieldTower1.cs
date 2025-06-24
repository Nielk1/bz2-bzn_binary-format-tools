﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "shieldtower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "shieldtower")]
    public class ClassShieldTower1Factory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassShieldTower1(preamble, classLabel);
            ClassShieldTower1.Hydrate(parent, reader, obj as ClassShieldTower1);
            return true;
        }
    }
    public class ClassShieldTower1 : ClassBuilding
    {
        public ClassShieldTower1(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassShieldTower1? obj)
        {
            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
