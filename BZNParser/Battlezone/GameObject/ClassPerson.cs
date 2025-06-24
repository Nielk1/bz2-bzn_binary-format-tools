﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "person")]
    [ObjectClass(BZNFormat.BattlezoneN64, "person")]
    [ObjectClass(BZNFormat.Battlezone2, "person")] // ?
    public class ClassPersonFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassPerson(preamble, classLabel);
            ClassPerson.Hydrate(parent, reader, obj as ClassPerson);
            return true;
        }
    }
    public class ClassPerson : ClassCraft
    {
        public float nextScream { get; set; }

        public ClassPerson(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassPerson? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("nextScream", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextScream/FLOAT");
                if (obj != null) obj.nextScream = tok.GetSingle();
            }
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version == 1047)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("nextScream", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextScream/FLOAT");
                    if (obj != null) obj.nextScream = tok.GetSingle();
                }
                else
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID"); // type not confirmed
                                                                                                                              //state = tok.GetUInt32H();

                    /*if (a2[2].vftable)
                    {
                        (a2->vftable->field_8)(a2, this + 2140, 4, "animMode");
                        (a2->vftable->field_8)(a2, this + 2144, 4, "animStance");
                        (a2->vftable->out_bool)(a2, this + 2200, 1, "fixedRate");
                        (a2->vftable->out_float)(a2, this + 2188, 4, "forceFps");
                        (a2->vftable->out_float)(a2, this + 2192, 4, "forceDir");
                        (a2->vftable->out_bool)(a2, this + 2201, 1, "wasFlying");
                        (a2->vftable->out_bool)(a2, this + 2202, 1, "Alive");
                        (a2->vftable->out_float)(a2, this + 2196, 4, "Dying_Timer");
                        (a2->vftable->out_bool)(a2, this + 2203, 1, "Explosion");
                    }*/
                }
            }

            ClassCraft.Hydrate(parent, reader, obj as ClassCraft);
        }
    }
}
