﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "deposit")]
    public class ClassDepositFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassDeposit(preamble, classLabel);
            ClassDeposit.Hydrate(parent, reader, obj as ClassDeposit);
            return true;
        }
    }
    public class ClassDeposit : ClassBuilding
    {
        public ClassDeposit(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassDeposit? obj)
        {
            IBZNToken tok;

            //if (reader.Format == BZNFormat.Battlezone2 && reader.Version > 1123)
            //{
            //    // unsure of the type of this
            //    tok = reader.ReadToken();
            //    if (!tok.Validate("saveClass", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse saveClass/CHAR");
            //    //saveClass = tok.GetString();
            //}

            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
