using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "tripmine")]
    public class ClassTripMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTripMine(PrjID, isUser, classLabel);
            ClassTripMine.Build(reader, obj as ClassTripMine);
            return true;
        }
    }
    public class ClassTripMine : ClassMine
    {
        public ClassTripMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassTripMine? obj)
        {
            ClassMine.Build(reader, obj as ClassMine);
        }
    }
}
