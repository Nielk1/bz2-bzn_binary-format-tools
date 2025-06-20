﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "recyclervehicleh")]
    [ObjectClass(BZNFormat.Battlezone2, "deploybuildingh")]
    public class ClassDeployBuildingHFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassDeployBuildingH(PrjID, isUser, classLabel);
            ClassDeployBuildingH.Hydrate(reader, obj as ClassDeployBuildingH);
            return true;
        }
    }
    public class ClassDeployBuildingH : ClassDeployable
    {
        public ClassDeployBuildingH(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNStreamReader reader, ClassDeployBuildingH? obj)
        {
            IBZNToken tok;

            if (reader.SaveType != 0) { }

            //if ( a2[2].vftable )
            //{
            //    (a2->vftable->out_bool)(a2, this + 2560, 1, "buildActive");
            //    (a2->vftable->out_float)(a2, this + 2576, 4, "buildTime");
            //}
            //(a2->vftable->field_1C)(a2, this + 2592, 64, "buildMatrix");
            tok = reader.ReadToken();
            if (!tok.Validate("buildMatrix", BinaryFieldType.DATA_MAT3D)) throw new Exception("Failed to parse buildMatrix/MAT3D"); // type unconfirmed
            //dropMat = tok.GetMatrix()

            ClassDeployable.Hydrate(reader, obj as ClassDeployable);
        }
    }
}
