using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "spawnpnt")]
    public class ClassSpawnBuoy2Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSpawnBuoy2(PrjID, isUser, classLabel);
            ClassSpawnBuoy2.Hydrate(reader, obj as ClassSpawnBuoy2);
            return true;
        }
    }
    public class ClassSpawnBuoy2 : ClassDummy
    {
        public ClassSpawnBuoy2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassSpawnBuoy2? obj)
        {
            ClassDummy.Hydrate(reader, obj as ClassDummy);
        }
    }
}
