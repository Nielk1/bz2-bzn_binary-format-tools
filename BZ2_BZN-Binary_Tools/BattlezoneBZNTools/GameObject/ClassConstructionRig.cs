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

        public ClassConstructionRig() { }
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
                if (!BZNFile.BZn64IdMap.ContainsKey(dropClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 dropClass enumeration 0x{0:2X} to string dropClass", dropClassItemID));
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
    }
}
