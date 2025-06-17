using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "sav")]
    [ObjectClass(BZNFormat.BattlezoneN64, "sav")]
    [ObjectClass(BZNFormat.Battlezone2, "sav")]
    public class ClassSAV : ClassHoverCraft
    {
        public ClassSAV(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
