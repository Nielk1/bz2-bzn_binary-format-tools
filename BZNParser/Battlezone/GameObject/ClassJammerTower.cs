using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "jammer")]
    public class ClassJammerTower : ClassPoweredBuilding
    {
        public ClassJammerTower(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
