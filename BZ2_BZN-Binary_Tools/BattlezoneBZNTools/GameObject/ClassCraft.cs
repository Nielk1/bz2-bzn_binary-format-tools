using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassCraft : ClassGameObject
    {
        public UInt32 abandoned { get; set; }

        public ClassCraft() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("abandoned", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse abandoned/LONG");
            abandoned = tok.GetUInt32();

            base.LoadData(reader);
        }
    }
}
