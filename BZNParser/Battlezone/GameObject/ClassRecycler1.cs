using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "recycler")]
    [ObjectClass(BZNFormat.BattlezoneN64, "recycler")]
    public class ClassRecycler1 : ClassProducer
    {
        public UInt32 undefptr { get; set; }

        public ClassRecycler1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr = tok.GetUInt32H(); // dropObj

            base.LoadData(reader);
        }
    }
}
