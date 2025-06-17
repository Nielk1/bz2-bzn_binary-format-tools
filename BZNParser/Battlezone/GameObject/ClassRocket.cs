using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassRocket : ClassBullet
    {
        public ClassRocket(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
