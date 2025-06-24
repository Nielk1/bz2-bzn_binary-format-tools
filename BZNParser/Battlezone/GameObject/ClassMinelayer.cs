﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "minelayer")]
    [ObjectClass(BZNFormat.BattlezoneN64, "minelayer")]
    [ObjectClass(BZNFormat.Battlezone2, "minelayer")]
    public class ClassMinelayerFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMinelayer(preamble, classLabel);
            ClassMinelayer.Hydrate(parent, reader, obj as ClassMinelayer);
            return true;
        }
    }
    public class ClassMinelayer : ClassHoverCraft
    {
        public ClassMinelayer(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassMinelayer? obj)
        {
            ClassHoverCraft.Hydrate(parent, reader, obj as ClassHoverCraft);
        }
    }
}
