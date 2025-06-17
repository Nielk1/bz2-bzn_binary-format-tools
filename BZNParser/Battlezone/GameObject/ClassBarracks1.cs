using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "barracks")]
    [ObjectClass(BZNFormat.BattlezoneN64, "barracks")]
    public class ClassBarracks1 : ClassBuilding
    {
        public ClassBarracks1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
