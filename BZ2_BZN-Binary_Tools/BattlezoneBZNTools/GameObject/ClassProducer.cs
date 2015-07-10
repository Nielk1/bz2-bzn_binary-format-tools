using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassProducer : ClassHoverCraft
    {
        public float timeDeploy { get; set; }
        public float timeUndeploy { get; set; }
        public UInt32 undefptr2 { get; set; }
        public UInt32 state { get; set; }
        //public UInt32 delayTimer { get; set; }
        public float delayTimer { get; set; }
        public float nextRepair { get; set; }
        public string buildClass { get; set; }
        //public UInt32 buildDoneTime { get; set; }
        public float buildDoneTime { get; set; }

        public ClassProducer() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("timeDeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeDeploy/FLOAT");
            timeDeploy = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("timeUndeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeUndeploy/FLOAT");
            timeUndeploy = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr2 = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            state = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("delayTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse delayTimer/FLOAT");
            delayTimer = tok.GetSingle();//tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("nextRepair", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextRepair/FLOAT");
            nextRepair = tok.GetSingle();

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 buildClassItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(buildClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 buildClass enumeration 0x{0:2X} to string buildClass", buildClassItemID));
                buildClass = BZNFile.BZn64IdMap[buildClassItemID];
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("buildClass", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse buildClass/ID");
                buildClass = tok.GetString();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("buildDoneTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildDoneTime/FLOAT");
            buildDoneTime = tok.GetSingle();

            base.LoadData(reader);
        }
    }
}
