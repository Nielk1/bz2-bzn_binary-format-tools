using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassConstructionRig : ClassProducer
    {
        public Matrix dropMat { get; set; }
        public string dropClass { get; set; }

        public ClassConstructionRig(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            if (reader.N64 || reader.Version > 1030)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("dropMat", BinaryFieldType.DATA_MAT3DOLD)) throw new Exception("Failed to parse dropMat/MAT3DOLD");
                dropMat = tok.GetMatrix();
            }

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 dropClassItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(dropClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 dropClass enumeration 0x(0:X2} to string dropClass", dropClassItemID));
                dropClass = BZNFile.BZn64IdMap[dropClassItemID];
            }
            else if (reader.Version > 1030)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("dropClass", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse dropClass/ID");
                dropClass = tok.GetString();
            }

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("dropMat [1] =");
            sb.AppendLine("  right_x [1] =");
            sb.AppendLine(dropMat.right.x.ToString());
            sb.AppendLine("  right_y [1] =");
            sb.AppendLine(dropMat.right.y.ToString());
            sb.AppendLine("  right_z [1] =");
            sb.AppendLine(dropMat.right.z.ToString());
            sb.AppendLine("  up_x [1] =");
            sb.AppendLine(dropMat.up.x.ToString());
            sb.AppendLine("  up_y [1] =");
            sb.AppendLine(dropMat.up.y.ToString());
            sb.AppendLine("  up_z [1] =");
            sb.AppendLine(dropMat.up.z.ToString());
            sb.AppendLine("  front_x [1] =");
            sb.AppendLine(dropMat.front.x.ToString());
            sb.AppendLine("  front_y [1] =");
            sb.AppendLine(dropMat.front.y.ToString());
            sb.AppendLine("  front_z [1] =");
            sb.AppendLine(dropMat.front.z.ToString());
            sb.AppendLine("  posit_x [1] =");
            sb.AppendLine(dropMat.posit.x.ToString());
            sb.AppendLine("  posit_y [1] =");
            sb.AppendLine(dropMat.posit.y.ToString());
            sb.AppendLine("  posit_z [1] =");
            sb.AppendLine(dropMat.posit.z.ToString());

            sb.AppendLine("dropClass [1] =");
            sb.AppendLine(dropClass.ToString());

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
