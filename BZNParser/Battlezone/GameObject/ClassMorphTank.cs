using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "morphtank")]
    public class ClassMorphTank : ClassDeployable
    {
        public ClassMorphTank(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            //tok = reader.ReadToken();
            //if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse soldierCount/LONG");
            //soldierCount = tok.GetUInt32();
            //
            //tok = reader.ReadToken();
            //if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            //state = tok.GetBytes(0, 4);

            base.LoadData(reader);
        }
    }
}
