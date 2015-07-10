using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassTug : ClassHoverCraft
    {
        public UInt32 undefptr { get; set; }

        public ClassTug() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse undefptr/LONG");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }
}
