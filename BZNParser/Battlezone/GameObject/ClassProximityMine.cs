using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "proximity")]
    [ObjectClass(BZNFormat.BattlezoneN64, "proximity")]
    [ObjectClass(BZNFormat.Battlezone2, "proximity")]
    public class ClassProximityMine : ClassMine
    {
        public ClassProximityMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
