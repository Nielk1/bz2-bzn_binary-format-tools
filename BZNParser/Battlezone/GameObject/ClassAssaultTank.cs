using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "assaulttank")]
    public class ClassAssaultTank : ClassTrackedVehicle
    {
        public ClassAssaultTank(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {


            base.LoadData(reader);
        }
    }
}
