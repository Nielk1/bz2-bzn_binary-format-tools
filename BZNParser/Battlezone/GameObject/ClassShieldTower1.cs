﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "shieldtower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "shieldtower")]
    public class ClassShieldTower1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassShieldTower1(PrjID, isUser, classLabel);
            ClassShieldTower1.Hydrate(reader, obj as ClassShieldTower1);
            return true;
        }
    }
    public class ClassShieldTower1 : ClassBuilding
    {
        public ClassShieldTower1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassShieldTower1? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
