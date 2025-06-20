﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "recyclervehicle")]
    public class ClassRecyclerVehicleFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRecyclerVehicle(PrjID, isUser, classLabel);
            ClassRecyclerVehicle.Build(reader, obj as ClassRecyclerVehicle);
            return true;
        }
    }
    public class ClassRecyclerVehicle : ClassDeployBuilding
    {
        public ClassRecyclerVehicle(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ref ClassRecyclerVehicle? obj)
        {
            if (reader.Version == 1047)
            {
                IBZNToken tok;

                tok = reader.ReadToken();
                if (!tok.Validate("nextRepair", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextRepair/FLOAT");
                float nextRepair = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate("buildDoneTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildDoneTime/FLOAT");

                tok = reader.ReadToken();
                if (!tok.Validate("buildActive", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse buildActive/BOOL");

                tok = reader.ReadToken();
                if (!tok.Validate("buildCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse buildCount/LONG");
                int buildCount = tok.GetInt32();

                for (int i = 0; i < buildCount; i++)
                {
                    //v5 = std::deque < GameObjectClass const *>::operator[] (v4);
                    //ILoadSaveVisitor::out(a2, *v5, "buildItem");
                    //++v4;
                    string item = reader.ReadGameObjectClass_BZ2("buildItem");
                }
            }

            ClassDeployBuilding.Build(reader, obj as ClassDeployBuilding);
        }
    }
}
