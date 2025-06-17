using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "artifact")]
    [ObjectClass(BZNFormat.BattlezoneN64, "artifact")]
    [ObjectClass(BZNFormat.Battlezone2, "artifact")]
    public class ClassArtifact : ClassBuilding
    {
        public ClassArtifact(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
