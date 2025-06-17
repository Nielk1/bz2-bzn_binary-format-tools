using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "supplydepot")]
    [ObjectClass(BZNFormat.BattlezoneN64, "supplydepot")]
    public class ClassSupplyDepot : ClassBuilding
    {
        public ClassSupplyDepot(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
