using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "geyser")]
    [ObjectClass(BZNFormat.BattlezoneN64, "geyser")]
    public class ClassGeizer : ClassBuilding
    {
        public UInt32 undefptr { get; set; }

        public ClassGeizer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
