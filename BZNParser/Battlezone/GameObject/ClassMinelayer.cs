using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "minelayer")]
    [ObjectClass(BZNFormat.BattlezoneN64, "minelayer")]
    [ObjectClass(BZNFormat.Battlezone2, "minelayer")]
    public class ClassMinelayer : ClassHoverCraft
    {
        public ClassMinelayer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
