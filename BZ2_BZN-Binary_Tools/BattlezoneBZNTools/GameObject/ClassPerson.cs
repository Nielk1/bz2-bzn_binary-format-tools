using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassPerson : ClassCraft
    {
        public float nextScream { get; set; }

        public ClassPerson(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("nextScream", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextScream/FLOAT");
            nextScream = tok.GetSingle();

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("nextScream [1] =");
            sb.AppendLine(nextScream.ToString());

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
