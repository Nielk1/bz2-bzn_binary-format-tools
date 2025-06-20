﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "shieldtower")]
    public class ClassShieldTower2Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassShieldTower2(PrjID, isUser, classLabel);
            ClassShieldTower2.Hydrate(reader, obj as ClassShieldTower2);
            return true;
        }
    }
    public class ClassShieldTower2 : ClassPoweredBuilding
    {
        public ClassShieldTower2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassShieldTower2? obj)
        {
            ClassPoweredBuilding.Hydrate(reader, obj as ClassPoweredBuilding);
        }
    }
}
