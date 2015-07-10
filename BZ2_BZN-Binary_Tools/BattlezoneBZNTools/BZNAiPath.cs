using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools
{
    public class BZNAiPath
    {
        public UInt32 old_ptr;
        public string label;
        public Vector2D[] points;
        public UInt32 pathType;

        public BZNAiPath(BZNReader reader)
        {
            IBZNToken tok;

            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AiPath", BinaryFieldType.DATA_UNKNOWN)) throw new Exception("Failed to parse [AiPath]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("old_ptr", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse old_ptr/VOID");
            old_ptr = tok.GetUInt32H();

            if (reader.N64)
            {
                tok = reader.ReadToken();
                uint pathValue = tok.GetUInt16();
                label = string.Format("bzn64path_{0:X4}", pathValue);
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("size", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse size/LONG");
                UInt32 size = tok.GetUInt32();

                if (size > 0)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("label", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse label/CHAR");
                    label = tok.GetString();
                }
            }

            tok = reader.ReadToken();
            if (!tok.Validate("pointCount", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse pointCount/LONG");
            UInt32 pointCount = tok.GetUInt32();

            points = new Vector2D[pointCount];

            tok = reader.ReadToken();
            if (!tok.Validate("points", BinaryFieldType.DATA_VEC2D)) throw new Exception("Failed to parse points/VEC2D");
            for (int pointCounter = 0; pointCounter < pointCount; pointCounter++)
            {
                points[pointCounter] = tok.GetVector2D(pointCounter);
            }
            //UInt32 points = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("pathType", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse pathType/VOID");
            pathType = tok.GetUInt32H();
        }
    }
}
