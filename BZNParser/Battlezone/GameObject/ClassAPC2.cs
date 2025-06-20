using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "apc")]
    public class ClassAPC2Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAPC2(PrjID, isUser, classLabel);
            ClassAPC2.Build(reader, obj as ClassAPC2);
            return true;
        }
    }
    public class ClassAPC2 : ClassHoverCraft
    {
        public ClassAPC2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassAPC2? obj)
        {
            IBZNToken tok;

            //(a2->vftable->read_long)(a2, this + 2336, 4, "IsoldierCount");
            tok = reader.ReadToken();
            if (!tok.Validate("IsoldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse IsoldierCount/LONG");
            //IsoldierCount = tok.GetUInt32();

            //(a2->vftable->read_long)(a2, this + 2352, 4, "EsoldierCount");
            tok = reader.ReadToken();
            if (!tok.Validate("EsoldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse EsoldierCount/LONG");
            int EsoldierCount = (int)tok.GetUInt32();

            if (EsoldierCount > 0)
            {
                //(a2->vftable->read_long)(a2, this + 2356, 4 * v3, "SoldierHandles");
                tok = reader.ReadToken();
                if (!tok.Validate("SoldierHandles", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse SoldierHandles/PTR");
                //tok.GetUInt32H();
            }
            //if (a2[2].vftable)
            //{
            //    (a2->vftable->out_float)(a2, this + 2340, 4, "nextSoldierDelay");
            //    (a2->vftable->out_float)(a2, this + 2344, 4, "nextSoldierAngle");
            //    (a2->vftable->out_float)(a2, this + 2348, 4, "nextReturnTimer");
            //    (a2->vftable->out_bool)(a2, this + 2420, 1, "DeployOnLanding");
            //    (a2->vftable->field_38)(a2, this + 2424, 4, "undeployTimeout");
            //}
            
            //(a2->vftable->field_8)(a2, this + 1424, 4, "state");
            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID))
                throw new Exception("Failed to parse state/VOID");
            //state = tok.GetBytes(0, 4);

            ClassHoverCraft.Build(reader, obj as ClassHoverCraft);
        }
    }
}
