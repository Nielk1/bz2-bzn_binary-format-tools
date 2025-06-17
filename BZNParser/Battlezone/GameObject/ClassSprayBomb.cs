using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spraybomb")]
    [ObjectClass(BZNFormat.BattlezoneN64, "spraybomb")]
    public class ClassSprayBomb : ClassGrenade
    {
        public ClassSprayBomb(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
