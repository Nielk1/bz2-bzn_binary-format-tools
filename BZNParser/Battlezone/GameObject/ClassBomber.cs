using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "bomber")]
    public class ClassBomber : ClassHoverCraft
    {
        public ClassBomber(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            // if reader.SaveType != 0
            // m_ReloadTime

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            UInt32 state = tok.GetUInt32H();

            // if reader.SaveType != 0
            // reloadTime float

            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
