using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassProducer : ClassHoverCraft
    {
        public float timeDeploy { get; set; }
        public float timeUndeploy { get; set; }
        public UInt32 undefptr2 { get; set; }
        public byte[] state { get; set; }
        //public UInt32 delayTimer { get; set; }
        public float delayTimer { get; set; }
        public float nextRepair { get; set; }
        public string buildClass { get; set; }
        //public UInt32 buildDoneTime { get; set; }
        public float buildDoneTime { get; set; }

        public ClassProducer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
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
            state = tok.GetBytes(0, 4);

            tok = reader.ReadToken();
            if (!tok.Validate("delayTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse delayTimer/FLOAT");
            delayTimer = tok.GetSingle();//tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("nextRepair", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextRepair/FLOAT");
            nextRepair = tok.GetSingle();

            if (reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                UInt16 buildClassItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(buildClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 buildClass enumeration 0x(0:X2} to string buildClass", buildClassItemID));
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

            if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version > 1022) { }
            else
            {
                tok = reader.ReadToken();//buildCost [1] =
                //-842150451
                tok = reader.ReadToken();//buildUpdateTime [1] =
                //-4.31602e+008
                tok = reader.ReadToken();//buildDt [1] =
                //-4.31602e+008
                tok = reader.ReadToken();//buildDc [1] =
                //-842150451
            }

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("timeDeploy [1] =");
            sb.AppendLine(timeDeploy.ToString());

            sb.AppendLine("timeUndeploy [1] =");
            sb.AppendLine(timeUndeploy.ToString());

            sb.AppendLine(string.Format("undefptr = {0:X8}", undefptr2));

            sb.AppendLine("state = " + BitConverter.ToString(state.Reverse().ToArray()).Replace("-", string.Empty));

            sb.AppendLine("delayTimer [1] =");
            sb.AppendLine(delayTimer.ToString());

            sb.AppendLine("nextRepair [1] =");
            sb.AppendLine(nextRepair.ToString());

            sb.AppendLine("buildClass [1] =");
            sb.AppendLine(buildClass.ToString());

            sb.AppendLine("buildDoneTime [1] =");
            sb.AppendLine(buildDoneTime.ToString());

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
