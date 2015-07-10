using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassPerson : ClassCraft
    {
        public UInt32 nextScream { get; set; }

        public ClassPerson() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("nextScream", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse nextScream/LONG");
            nextScream = tok.GetUInt32();

            base.LoadData(reader);
        }
    }
}
