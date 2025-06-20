using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "geyser")]
    [ObjectClass(BZNFormat.BattlezoneN64, "geyser")]
    public class ClassGeizerFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassGeizer(PrjID, isUser, classLabel);
            ClassGeizer.Hydrate(reader, obj as ClassGeizer);
            return true;
        }
    }
    public class ClassGeizer : ClassBuilding
    {
        public UInt32 undefptr { get; set; }

        public ClassGeizer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassGeizer? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}
