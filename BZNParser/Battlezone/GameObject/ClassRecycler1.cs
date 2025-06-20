﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "recycler")]
    [ObjectClass(BZNFormat.BattlezoneN64, "recycler")]
    public class ClassRecycler1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRecycler1(PrjID, isUser, classLabel);
            ClassRecycler1.Hydrate(reader, obj as ClassRecycler1);
            return true;
        }
    }
    public class ClassRecycler1 : ClassProducer
    {
        public UInt32 undefptr { get; set; }

        public ClassRecycler1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassRecycler1? obj)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            if (obj != null) obj.undefptr = tok.GetUInt32H(); // dropObj

            ClassProducer.Hydrate(reader, obj as ClassProducer);
        }
    }
}
