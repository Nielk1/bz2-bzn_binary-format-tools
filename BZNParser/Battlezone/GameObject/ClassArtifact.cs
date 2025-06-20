using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "artifact")]
    [ObjectClass(BZNFormat.BattlezoneN64, "artifact")]
    [ObjectClass(BZNFormat.Battlezone2, "artifact")]
    public class ClassArtifactFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassArtifact(PrjID, isUser, classLabel);
            ClassArtifact.Hydrate(reader, obj as ClassArtifact);
            return true;
        }
    }
    public class ClassArtifact : ClassBuilding
    {
        public ClassArtifact(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNStreamReader reader, ClassArtifact? obj)
        {
            ClassBuilding.Hydrate(reader, obj as ClassBuilding);
        }
    }
}