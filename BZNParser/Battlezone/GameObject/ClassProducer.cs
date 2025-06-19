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
        //public byte[] state { get; set; }
        public UInt32 state { get; set; }
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

            if (reader.Format == BZNFormat.Battlezone && reader.Version < 1011)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("setAltitude", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse setAltitude/FLOAT");
                float setAltitude = tok.GetSingle();
            }

            if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version != 1042)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("timeDeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeDeploy/FLOAT");
                timeDeploy = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate("timeUndeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeUndeploy/FLOAT");
                timeUndeploy = tok.GetSingle();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr2 = tok.GetUInt32H(); // powerSource

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            //state = tok.GetBytes(0, 4); // probably need to reverse for n64
            state = tok.GetUInt32(); // probably need to reverse for n64

            tok = reader.ReadToken();
            if (!tok.Validate("delayTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse delayTimer/FLOAT");
            delayTimer = tok.GetSingle();//tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("nextRepair", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextRepair/FLOAT");
            nextRepair = tok.GetSingle();

            if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version >= 1006)
            {
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
                // BZn64 might be invalid when it has CDCDCDCA here.

                if (reader.Format == BZNFormat.Battlezone && reader.Version <= 1026)
                {
                    // dummied out and unused
                    tok = reader.ReadToken();//buildCost [1] =
                                             //-842150451
                    tok = reader.ReadToken();//buildUpdateTime [1] =
                                             //-4.31602e+008
                    tok = reader.ReadToken();//buildDt [1] =
                                             //-4.31602e+008
                    tok = reader.ReadToken();//buildDc [1] =
                                             //-842150451
                }
            }

            base.LoadData(reader);
        }
    }
}
