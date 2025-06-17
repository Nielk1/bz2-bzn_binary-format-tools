using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "powerplant")]
    [ObjectClass(BZNFormat.BattlezoneN64, "powerplant")]
    [ObjectClass(BZNFormat.Battlezone2, "powerplant")]
    [ObjectClass(BZNFormat.Battlezone2, "powerlung")]
    public class ClassPowerPlant : ClassBuilding
    {
        public ClassPowerPlant(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
