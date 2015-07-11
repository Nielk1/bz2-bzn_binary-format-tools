using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassScrapSilo : ClassGameObject
    {
        public UInt32 undefptr { get; set; }

        public ClassScrapSilo(string PrjID, bool isUser) : base(PrjID, isUser) { }

        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/LONG");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }

        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("undefptr [1] = {0:X8}", undefptr));

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
