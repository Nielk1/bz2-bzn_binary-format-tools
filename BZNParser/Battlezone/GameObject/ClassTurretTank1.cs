using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "turrettank")]
    [ObjectClass(BZNFormat.BattlezoneN64, "turrettank")]
    public class ClassTurretTank1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTurretTank1(PrjID, isUser, classLabel);
            ClassTurretTank1.Hydrate(reader, obj as ClassTurretTank1);
            return true;
        }
    }
    public class ClassTurretTank1 : ClassHoverCraft
    {
        public float undeffloat1 { get; set; }
        public float undeffloat2 { get; set; }
        public float undeffloat3 { get; set; }
        public float undeffloat4 { get; set; }
        public UInt32 undefraw { get; set; }
        public float undeffloat5 { get; set; }
        public bool undefbool { get; set; }

        public ClassTurretTank1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassTurretTank1? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version > 1000)
                {
                    if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version != 1042)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                        if (obj != null) obj.undeffloat1 = tok.GetSingle(); // omegaTurret

                        tok = reader.ReadToken();
                        if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                        if (obj != null) obj.undeffloat2 = tok.GetSingle(); // alphaTurret

                        tok = reader.ReadToken();
                        if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                        if (obj != null) obj.undeffloat3 = tok.GetSingle(); // timeDeploy

                        tok = reader.ReadToken();
                        if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                        if (obj != null) obj.undeffloat4 = tok.GetSingle(); // timeUndeploy
                    }

                    tok = reader.ReadToken();
                    if (!tok.Validate("undefraw", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse undefraw/VOID");
                    if (obj != null) obj.undefraw = tok.GetUInt32(); // state

                    tok = reader.ReadToken();
                    if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                    if (obj != null) obj.undeffloat5 = tok.GetSingle(); // delayTimer

                    if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version != 1042)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("undefbool", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse undefbool/BOOL");
                        if (obj != null) obj.undefbool = tok.GetBoolean(); // wantTurret
                    }
                }
            }

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
