﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "bomberbay")]
    public class ClassBomberBayFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBomberBay(preamble, classLabel);
            ClassBomberBay.Hydrate(parent, reader, obj as ClassBomberBay);
            return true;
        }
    }
    public class ClassBomberBay : ClassPoweredBuilding
    {
        protected int m_MyBomber { get; set; }
        public ClassBomberBay(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassBomberBay? obj)
        {
            IBZNToken tok;

            if (reader.Version >= 1131)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("Handle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse Handle/LONG");
                if (obj != null) obj.m_MyBomber = tok.GetInt32();
            }
            else
            {
                if (obj != null)
                {
                    obj.m_MyBomber = 0;
                    // find bomber via TEAM_SLOT_BOMBER scan
                    // if this is mid load doesn't that require the bomber come first in the BZN file? Maybe do this in a post-load step or write a malformation that tries to auto-fix?
                }
            }

            ClassPoweredBuilding.Hydrate(parent, reader, obj as ClassPoweredBuilding);
        }
    }
}
