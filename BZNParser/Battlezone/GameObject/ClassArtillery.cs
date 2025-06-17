using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "artillery")]
    public class ClassArtillery : ClassTurretTank2
    {
        public ClassArtillery(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Version < 1110)
            {
                IBZNToken tok;

                tok = reader.ReadToken();
                if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
                UInt32 state = tok.GetUInt32H();

                // block of reader.SaveType != 0

                // use hovercraft instead of turretcraft here
            }
            else
            {
                // block of reader.SaveType != 0
            }

            base.LoadData(reader);
        }
    }
}
