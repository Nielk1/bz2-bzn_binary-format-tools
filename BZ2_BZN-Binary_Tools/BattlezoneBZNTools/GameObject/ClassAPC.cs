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
        public UInt32 state { get; set; }

        public ClassAPC() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;
            
            tok = reader.ReadToken();
            if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse soldierCount/LONG");
            soldierCount = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse state/LONG");
            state = tok.GetUInt32();

            base.LoadData(reader);
        }
    }
}
