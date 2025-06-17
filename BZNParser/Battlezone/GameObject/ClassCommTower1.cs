using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "commtower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "commtower")]
    public class ClassCommTower1 : ClassBuilding
    {
        public ClassCommTower1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
