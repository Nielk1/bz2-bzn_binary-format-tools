﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "morphtank")]
    public class ClassMorphTankFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMorphTank(preamble, classLabel);
            ClassMorphTank.Hydrate(parent, reader, obj as ClassMorphTank);
            return true;
        }
    }
    public class ClassMorphTank : ClassDeployable
    {
        public ClassMorphTank(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassMorphTank? obj)
        {
            IBZNToken tok;

            //tok = reader.ReadToken();
            //if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse soldierCount/LONG");
            //soldierCount = tok.GetUInt32();
            //
            //tok = reader.ReadToken();
            //if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            //state = tok.GetBytes(0, 4);

            ClassDeployable.Hydrate(parent, reader, obj as ClassDeployable);
        }
    }
}
