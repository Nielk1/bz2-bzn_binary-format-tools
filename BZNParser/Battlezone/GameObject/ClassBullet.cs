using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassBullet : ClassOrdnance
    {
        public ClassBullet(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
