using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassAPC : ClassHoverCraft
    {
        public UInt32 soldierCount { get; set; }
        public byte[] state { get; set; }

        public ClassAPC(string PrjID, bool isUser) : base(PrjID, isUser) { }

        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;
            
            tok = reader.ReadToken();
            if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse soldierCount/LONG");
            soldierCount = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            state = tok.GetRaw(0, 4);

            base.LoadData(reader);
        }

        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("soldierCount [1] =");
            sb.AppendLine(soldierCount.ToString());

            sb.AppendLine("state = " + BitConverter.ToString(state.Reverse().ToArray()).Replace("-", string.Empty));

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
