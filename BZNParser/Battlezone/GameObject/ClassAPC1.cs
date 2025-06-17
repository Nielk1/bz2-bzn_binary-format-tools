using BZNParser.Reader;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "apc")]
    [ObjectClass(BZNFormat.BattlezoneN64, "apc")]
    public class ClassAPC1 : ClassHoverCraft
    {
        public UInt32 soldierCount { get; set; }
        public byte[] state { get; set; }

        public ClassAPC1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;
            
            tok = reader.ReadToken();
            if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse soldierCount/LONG");
            soldierCount = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID))
                throw new Exception("Failed to parse state/VOID");
            state = tok.GetBytes(0, 4);

            base.LoadData(reader);
        }
    }
}
