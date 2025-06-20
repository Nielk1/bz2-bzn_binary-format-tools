using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassOrdnance : ClassGameObject // TODO this linkage to GameObject is fake just to get us compiling, when we add logic for saves we will need to revisit this and maybe make the entity root object
    {
        public ClassOrdnance(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassGameObject? obj)
        {
        }
    }
}
