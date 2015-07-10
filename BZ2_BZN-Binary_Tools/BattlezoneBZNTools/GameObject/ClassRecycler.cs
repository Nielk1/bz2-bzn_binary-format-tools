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

        public ClassRecycler() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }
}
