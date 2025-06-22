﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "apc")]
    public class ClassAPC2Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAPC2(PrjID, isUser, classLabel);
            ClassAPC2.Hydrate(reader, obj as ClassAPC2);
            return true;
        }
    }
    public class ClassAPC2 : ClassHoverCraft
    {
        const int APC_MAX_SOLDIERS = 16;

        public int InternalSoldierCount { get; set; }
        public float nextSoldierDelay { get; set; }
        public float nextSoldierAngle { get; set; }
        public float nextReturnToAPC { get; set; }
        public int ExternalSoldierCount { get; set; }
        public int[] ExternalSoldiers { get; set; }
        public bool DeployOnLanding { get; set; }
        public long undeployTimeout { get; set; }

        public ClassAPC2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassAPC2? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("IsoldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse IsoldierCount/LONG");
            if (obj != null) obj.InternalSoldierCount = tok.GetInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("EsoldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse EsoldierCount/LONG");
            int ExternalSoldierCount = tok.GetInt32();
            if (obj != null) obj.ExternalSoldierCount = ExternalSoldierCount;

            if (ExternalSoldierCount > 0)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("SoldierHandles", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse SoldierHandles/PTR");
                //tok.GetUInt32H();
                if (obj != null)
                {
                    int count = tok.GetCount();
                    if (count > APC_MAX_SOLDIERS)
                        obj.Malformations.Add(Malformation.OVERCOUNT, "ExternalSoldiers");
                    obj.ExternalSoldiers = new int[Math.Max(APC_MAX_SOLDIERS, count)];
                    for(int i = 0; i < count; i++)
                    {
                        obj.ExternalSoldiers[i] = tok.GetInt32(i);
                    }
                }
            }
            
            if (reader.SaveType != 0)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("nextSoldierDelay", BinaryFieldType.DATA_FLOAT))
                    throw new Exception("Failed to parse nextSoldierDelay/FLOAT");
                if (obj != null) obj.nextSoldierDelay = tok.GetSingle(); // nextSoldierDelay

                tok = reader.ReadToken();
                if (!tok.Validate("nextSoldierAngle", BinaryFieldType.DATA_FLOAT))
                    throw new Exception("Failed to parse nextSoldierAngle/FLOAT");
                if (obj != null) obj.nextSoldierAngle = tok.GetSingle(); // nextSoldierAngle

                tok = reader.ReadToken();
                if (!tok.Validate("nextReturnTimer", BinaryFieldType.DATA_FLOAT))
                    throw new Exception("Failed to parse nextReturnTimer/FLOAT");
                if (obj != null) obj.nextReturnToAPC = tok.GetSingle(); // nextReturnTimer

                tok = reader.ReadToken();
                if (!tok.Validate("DeployOnLanding", BinaryFieldType.DATA_BOOL))
                    throw new Exception("Failed to parse DeployOnLanding/BOOL");
                if (obj != null) obj.DeployOnLanding = tok.GetBoolean(); // DeployOnLanding

                tok = reader.ReadToken();
                if (!tok.Validate("undeployTimeout", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse undeployTimeout/LONG");
                if (obj != null) obj.undeployTimeout = tok.GetInt32(); // undeployTimeout
            }
            
            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            if (obj != null) obj.state = (VEHICLE_STATE)tok.GetUInt32(); // state

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
