using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "portal")]
    public class ClassPortal : ClassGameObject
    {
        public ClassPortal(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2016)
            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2011)
            if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2004)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("portalState", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse portalState/LONG");
                UInt32 portalState = tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("portalBeginTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse portalBeginTime/FLOAT");
                float portalBeginTime = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate("portalEndTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse portalEndTime/FLOAT");
                float portalEndTime = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate("isIn", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse isIn/BOOL");
                bool isIn = tok.GetBoolean();
            }

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
