using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools
{
    public class BZNAOI
    {
        public UInt32 undefptr;
        public UInt32 team;
        public bool interesting;
        public bool inside;
        public UInt32 value;
        public UInt32 force;

        public BZNAOI(BZNReader reader)
        {
            IBZNToken tok;

            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AOI")) throw new Exception("Failed to parse [AOI]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse undefptr/LONG");
            UInt32 undefptr = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("team", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse team/LONG");
            UInt32 team = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("interesting", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse interesting/BOOL");
            bool interesting = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("inside", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse inside/BOOL");
            bool inside = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("value", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse value/LONG");
            UInt32 value = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("force", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse force/LONG");
            UInt32 force = tok.GetUInt32();
        }
    }
}
