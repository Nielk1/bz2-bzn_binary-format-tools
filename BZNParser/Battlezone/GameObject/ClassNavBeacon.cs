using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "beacon")]
    public class ClassNavBeacon : ClassGameObject
    {
        public ClassNavBeacon(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                throw new Exception("Failed to parse name/CHAR");
            name = tok.GetString();

            tok = reader.ReadToken();
            if (!tok.Validate("navSlot", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse navSlot/LONG");
            //int navSlot = tok.GetInt32();

            base.LoadData(reader);
        }
    }
}
