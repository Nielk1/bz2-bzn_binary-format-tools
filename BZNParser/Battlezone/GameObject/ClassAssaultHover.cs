using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "assaulthover")]
    public class ClassAssaultHoverFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAssaultHover(PrjID, isUser, classLabel);
            ClassAssaultHover.Hydrate(reader, obj as ClassAssaultHover);
            return true;
        }
    }
    public class ClassAssaultHover : ClassHoverCraft
    {
        public ClassAssaultHover(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNStreamReader reader, ClassAssaultHover? obj)
        {
            if (reader.SaveType != 0)
            {

            }

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
