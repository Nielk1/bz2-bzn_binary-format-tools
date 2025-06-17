using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "deployable")]
    public class ClassDeployable : ClassHoverCraft
    {
        public ClassDeployable(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Format == BZNFormat.Battlezone2 && reader.Version < 1110 && ClassLabel == "artillery")
            {
                base.LoadData(reader);
                return;
            }
            if (reader.Format == BZNFormat.Battlezone2 && reader.Version < 1109 && ClassLabel == "turrettank")
            {
                base.LoadData(reader);
                return;
            }

            IBZNToken tok;

            // this class doesn't exist in BZ1
            //if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID"); // type not confirmed
                UInt32 state = tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("deployTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse deployTimer/FLOAT");
                float deployTimer = tok.GetSingle();

                if (reader.SaveType == 0)
                {
                    // setup stuff where some vars are generated
                }
                else
                {
                    /*if (a2[2].vftable)
                    {
                        (a2->vftable->read_long)(a2, this + 2336, 4, "changeState");
                        (a2->vftable->out_bool)(a2, this + 2344, 1, "lockMode");
                        (a2->vftable->out_bool)(a2, this + 2345, 1, "lockModeDeployed");
                    }*/
                }
            }

            base.LoadData(reader);
        }
    }
}
