﻿using BZNParser.Reader;
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
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassFactory1(PrjID, isUser, classLabel);
            ClassFactory1.Hydrate(reader, obj as ClassFactory1);
            return true;
        }
    }
    public class ClassFactory1 : ClassProducer
    {
        public ClassFactory1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassFactory1? obj)
        {
            ClassProducer.Hydrate(reader, obj as ClassProducer);
        }
    }
}
