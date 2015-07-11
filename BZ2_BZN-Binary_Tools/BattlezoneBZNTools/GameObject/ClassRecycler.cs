using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassRecycler : ClassProducer
    {
        public UInt32 undefptr { get; set; }

        public ClassRecycler(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("undefptr [1] =");
            sb.AppendLine(string.Format("{0:X8}", undefptr.ToString()));

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
