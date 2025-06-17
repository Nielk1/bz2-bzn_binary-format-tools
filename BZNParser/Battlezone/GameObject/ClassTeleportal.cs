using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "teleportal")]
    public class ClassTeleportal : ClassPoweredBuilding
    {
        public ClassTeleportal(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
