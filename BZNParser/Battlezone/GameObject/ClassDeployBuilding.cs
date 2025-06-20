﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "deploybuilding")]
    public class ClassDeployBuildingFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassDeployBuilding(PrjID, isUser, classLabel);
            ClassDeployBuilding.Hydrate(reader, obj as ClassDeployBuilding);
            return true;
        }
    }
    public class ClassDeployBuilding : ClassTrackedDeployable
    {
        public ClassDeployBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNStreamReader reader, ClassDeployBuilding? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version != 1047)
                {
                    //if ( a2[2].vftable )
                    //{
                    //    (a2->vftable->out_bool)(a2, this + 2560, 1, "buildActive");
                    //    (a2->vftable->out_float)(a2, this + 2576, 4, "buildTime");
                    //}
                    //(a2->vftable->field_1C)(a2, this + 2592, 64, "buildMatrix");
                    tok = reader.ReadToken();
                    if (!tok.Validate("buildMatrix", BinaryFieldType.DATA_MAT3D)) throw new Exception("Failed to parse buildMatrix/MAT3D"); // type unconfirmed
                                                                                                                                            //dropMat = tok.GetMatrix()
                }
            }

            ClassTrackedDeployable.Hydrate(reader, obj as ClassTrackedDeployable);
        }
    }
}
