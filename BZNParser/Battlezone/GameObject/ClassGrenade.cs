using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassGrenade : ClassRocket
    {
        public ClassGrenade(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
