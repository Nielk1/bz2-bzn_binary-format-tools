using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "service")]
    public class ClassServiceTruck : ClassTrackedVehicle
    {
        public ClassServiceTruck(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
