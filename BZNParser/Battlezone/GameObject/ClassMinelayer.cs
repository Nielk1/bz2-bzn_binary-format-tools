﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "minelayer")]
    [ObjectClass(BZNFormat.BattlezoneN64, "minelayer")]
    [ObjectClass(BZNFormat.Battlezone2, "minelayer")]
    public class ClassMinelayerFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMinelayer(PrjID, isUser, classLabel);
            ClassMinelayer.Hydrate(reader, obj as ClassMinelayer);
            return true;
        }
    }
    public class ClassMinelayer : ClassHoverCraft
    {
        public ClassMinelayer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassMinelayer? obj)
        {
            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
