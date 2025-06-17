using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    // BZ2
    public class ClassTrackedDeployable : ClassTrackedVehicle
    {
        public ClassTrackedDeployable(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            //(a2->vftable->field_8)(a2, this + 1424, 4, "state");
            {
                tok = reader.ReadToken();
                if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID"); // type not confirmed
                //state = tok.GetUInt32H();

                //(a2->vftable->out_float)(a2, this + 2548, 4, "deployTimer");
                tok = reader.ReadToken();
                if (!tok.Validate("deployTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse deployTimer/FLOAT");
                //deployTimer = tok.GetSingle();

                //if (a2[2].vftable)
                //    (a2->vftable->read_long)(a2, this + 2544, 4, "changeState");
            }

            base.LoadData(reader);
        }
    }
}
