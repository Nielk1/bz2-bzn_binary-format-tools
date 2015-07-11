using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassScavenger : ClassHoverCraft
    {
        public UInt32 scrapHeld { get; set; }

        public ClassScavenger(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            if (!reader.N64)
            {
                if (reader.Version > 1034)
                {
                    IBZNToken tok = reader.ReadToken();
                    if (!tok.Validate("scrapHeld", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse scrapHeld/LONG");
                    scrapHeld = tok.GetUInt32();
                }
            }

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("scrapHeld [1] =");
            sb.AppendLine(scrapHeld.ToString());

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
