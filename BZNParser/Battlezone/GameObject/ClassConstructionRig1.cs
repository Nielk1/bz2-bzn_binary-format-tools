using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "constructionrig")]
    [ObjectClass(BZNFormat.BattlezoneN64, "constructionrig")]
    public class ClassConstructionRig1 : ClassProducer
    {
        public Matrix dropMat { get; set; }
        public string dropClass { get; set; }

        public ClassConstructionRig1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone && reader.Version <= 1030)
            {
                // block 40 byte read, probably a raw matrix of some kind
                tok = reader.ReadToken();
                if (!tok.Validate("dropMat", BinaryFieldType.DATA_MAT3DOLD)) throw new Exception("Failed to parse dropMat/MAT3D");
                dropMat = tok.GetMatrix();
            }
            else
            {
                if (reader.Format == BZNFormat.BattlezoneN64 || (reader.Format == BZNFormat.Battlezone && reader.Version > 1030))
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("dropMat", BinaryFieldType.DATA_MAT3DOLD)) throw new Exception("Failed to parse dropMat/MAT3DOLD");
                    dropMat = tok.GetMatrixOld();
                }
            }

            if (reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                UInt16 dropClassItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(dropClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 dropClass enumeration 0x(0:X2} to string dropClass", dropClassItemID));
                dropClass = BZNFile.BZn64IdMap[dropClassItemID];
            }
            else if (reader.Format == BZNFormat.Battlezone && reader.Version > 1030)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("dropClass", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse dropClass/ID");
                dropClass = tok.GetString();
            }

            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2016)
            //if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2011)
            if (reader.Format == BZNFormat.Battlezone && reader.Version >= 2001)
            {
                tok = reader.ReadToken();
                //if (!tok.Validate("lastRecycled", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse lastRecycled/FLOAT");
                if (!tok.Validate("lastRecycled", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse lastRecycled/LONG");
                //lastRecycled = tok.GetSingle();
            }

            base.LoadData(reader);
        }
    }
}
