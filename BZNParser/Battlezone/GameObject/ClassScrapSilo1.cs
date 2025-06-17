using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrapsilo")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrapsilo")]
    public class ClassScrapSilo1 : ClassGameObject
    {
        public UInt32 undefptr { get; set; }

        public ClassScrapSilo1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/LONG");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }
}
