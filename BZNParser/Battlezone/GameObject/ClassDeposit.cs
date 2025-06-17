using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "deposit")]
    public class ClassDeposit : ClassBuilding
    {
        public ClassDeposit(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            //if (reader.Format == BZNFormat.Battlezone2 && reader.Version > 1123)
            //{
            //    // unsure of the type of this
            //    tok = reader.ReadToken();
            //    if (!tok.Validate("saveClass", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse saveClass/CHAR");
            //    //saveClass = tok.GetString();
            //}

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
