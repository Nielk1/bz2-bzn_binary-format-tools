using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "shieldtower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "shieldtower")]
    public class ClassShieldTower1 : ClassBuilding
    {
        public ClassShieldTower1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
