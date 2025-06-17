using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "daywrecker")]
    [ObjectClass(BZNFormat.BattlezoneN64, "daywrecker")]
    [ObjectClass(BZNFormat.Battlezone2, "daywrecker")]
    public class ClassDayWrecker : ClassPowerUp
    {
        public ClassDayWrecker(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
