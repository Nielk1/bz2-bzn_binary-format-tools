using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "servicepod")]
    [ObjectClass(BZNFormat.Battlezone2, "ammopack")]
    [ObjectClass(BZNFormat.Battlezone2, "repairkit")]
    public class ClassServicePowerup : ClassPowerUp
    {
        public ClassServicePowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
