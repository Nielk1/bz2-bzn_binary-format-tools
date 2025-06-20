using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "serviceh")]
    public class ClassServiceTruckHFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassServiceTruckH(PrjID, isUser, classLabel);
            ClassServiceTruckH.Hydrate(reader, obj as ClassServiceTruckH);
            return true;
        }
    }
    public class ClassServiceTruckH : ClassHoverCraft
    {
        public ClassServiceTruckH(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassServiceTruckH? obj)
        {
            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
