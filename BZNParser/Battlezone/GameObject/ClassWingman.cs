﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "wingman")]
    [ObjectClass(BZNFormat.BattlezoneN64, "wingman")]
    [ObjectClass(BZNFormat.Battlezone2, "wingman")]
    public class ClassWingmanFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassWingman(PrjID, isUser, classLabel);
            ClassWingman.Hydrate(reader, obj as ClassWingman);
            return true;
        }
    }
    public class ClassWingman : ClassHoverCraft
    {
        public ClassWingman(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassWingman? obj)
        {
            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
