using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "sav")]
    [ObjectClass(BZNFormat.BattlezoneN64, "sav")]
    [ObjectClass(BZNFormat.Battlezone2, "sav")]
    public class ClassSAVFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSAV(PrjID, isUser, classLabel);
            ClassSAV.Hydrate(parent, reader, obj as ClassSAV);
            return true;
        }
    }
    public class ClassSAV : ClassHoverCraft
    {
        public ClassSAV(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassSAV? obj)
        {
            ClassHoverCraft.Hydrate(parent, reader, obj as ClassHoverCraft);
        }
    }
}
