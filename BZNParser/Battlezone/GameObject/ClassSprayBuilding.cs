using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spraybomb")]
    [ObjectClass(BZNFormat.BattlezoneN64, "spraybomb")]
    [ObjectClass(BZNFormat.Battlezone2, "spraybomb")]
    public class ClassSprayBuilding : ClassGrenade
    {
        public ClassSprayBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
