﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scavenger")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scavenger")]
    public class ClassScavenger1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScavenger1(PrjID, isUser, classLabel);
            ClassScavenger1.Hydrate(reader, obj as ClassScavenger1);
            return true;
        }
    }
    public class ClassScavenger1 : ClassHoverCraft
    {
        public UInt32 scrapHeld { get; set; }

        public ClassScavenger1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassScavenger1? obj)
        {
            if (reader.Format == BZNFormat.Battlezone)
            {
                //if (reader.Version > 1034)
                //if (reader.Version > 1037)
                if ((reader.Version >= 1039 && reader.Version < 2000) || reader.Version > 2004) // confirms from BZ98R disassembly, not confirmed against BZCC yet
                {
                    IBZNToken tok = reader.ReadToken();
                    if (!tok.Validate("scrapHeld", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse scrapHeld/LONG");
                    if (obj != null) obj.scrapHeld = tok.GetUInt32();
                }
            }

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
