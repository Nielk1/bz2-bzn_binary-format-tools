using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "tug")]
    [ObjectClass(BZNFormat.BattlezoneN64, "tug")]
    [ObjectClass(BZNFormat.Battlezone2, "tug")]
    public class ClassTug : ClassHoverCraft
    {
        public UInt32 undefptr { get; set; }

        public ClassTug(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
                undefptr = tok.GetUInt32H(); // cargo
            }
            else if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version < 1109)
                {
                    if (reader.SaveType != 0)
                    {
                        // 2 things to read here, cargoHandle and lastPosit
                    }
                }
            }

            base.LoadData(reader);

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1109)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("state", BinaryFieldType.DATA_VOID))
                        throw new Exception("Failed to parse state/VOID");
                    // state = tok.GetUInt32H();

                    tok = reader.ReadToken();
                    if (!tok.Validate("cargoHandle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse cargoHandle/LONG");

                    if (reader.SaveType != 0)
                    {
                        //(a2->vftable->field_24)(a2, this + 2364, 12, "lastPosit");
                    }
                }
                if (reader.SaveType == 2 || reader.SaveType == 3)
                {
                    //(a2->vftable->out_float)(a2, this + 2340, 4, "dockSpeed");
                    //(a2->vftable->out_float)(a2, this + 2344, 4, "delayTimer");
                    //(a2->vftable->out_float)(a2, this + 2348, 4, "timeDeploy");
                    //(a2->vftable->out_float)(a2, this + 2352, 4, "timeUndeploy");
                }
                if (reader.SaveType == 0)
                {
                    // stuff
                }
            }
        }
    }
}
