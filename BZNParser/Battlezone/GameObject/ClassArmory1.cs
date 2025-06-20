using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "armory")]
    [ObjectClass(BZNFormat.BattlezoneN64, "armory")]
    public class ClassArmory1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassArmory1(PrjID, isUser, classLabel);
            ClassArmory1.Hydrate(reader, obj as ClassArmory1);
            return true;
        }
    }
    public class ClassArmory1 : ClassProducer
    {
        public ClassArmory1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassArmory1? obj)
        {
            ClassProducer.Hydrate(reader, obj as ClassProducer);
        }
    }
}
