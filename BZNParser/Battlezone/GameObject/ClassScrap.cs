﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrap")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrap")]
    [ObjectClass(BZNFormat.Battlezone2, "scrap")]
    public class ClassScrapFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScrap(preamble, classLabel);
            ClassScrap.Hydrate(parent, reader, obj as ClassScrap);
            return true;
        }
    }
    public class ClassScrap : ClassGameObject
    {
        public ClassScrap(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassScrap? obj)
        {
            //if (a2->isSave)
            //{
            //    (a2->vftable->field_38)(a2, &this[1].gap8[52], 4, "expireTime");
            //    (a2->vftable->out_bool)(a2, &this[1].gap8[48], 1, "HardToGetTo");
            //}
            ClassGameObject.Hydrate(parent, reader, obj as ClassGameObject);
        }
    }
    
}
