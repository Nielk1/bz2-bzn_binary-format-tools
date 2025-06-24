using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "factory")]
    [ObjectClass(BZNFormat.BattlezoneN64, "factory")]
    public class ClassFactory1Factory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassFactory1(preamble, classLabel);
            ClassFactory1.Hydrate(parent, reader, obj as ClassFactory1);
            return true;
        }
    }
    public class ClassFactory1 : ClassProducer
    {
        public ClassFactory1(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassFactory1? obj)
        {
            ClassProducer.Hydrate(parent, reader, obj as ClassProducer);
        }
    }
}
