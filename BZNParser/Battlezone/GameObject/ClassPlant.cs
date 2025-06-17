using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "plant")]
    public class ClassPlant : ClassBuilding
    {
        public ClassPlant(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            base.LoadData(reader);
        }
    }
}
