using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrapfield")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrapfield")]
    [ObjectClass(BZNFormat.Battlezone2, "scrapfield")]
    public class ClassScrapField : ClassBuilding
    {
        public ClassScrapField(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
