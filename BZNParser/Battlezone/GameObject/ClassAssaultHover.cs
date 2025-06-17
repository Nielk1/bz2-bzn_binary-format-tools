using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "assaulthover")]
    public class ClassAssaultHover : ClassHoverCraft
    {
        public ClassAssaultHover(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.SaveType != 0)
            {

            }

            base.LoadData(reader);
        }
    }
}
