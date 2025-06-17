using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "bomberbay")]
    public class ClassBomberBay : ClassPoweredBuilding
    {
        public ClassBomberBay(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Version >= 1131)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("Handle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse Handle/LONG");
                //int buildCount = tok.GetInt32();
                // m_MyBomber
            }
            else
            {
                // find bomber via slot scan
            }

            base.LoadData(reader);
        }
    }
}
