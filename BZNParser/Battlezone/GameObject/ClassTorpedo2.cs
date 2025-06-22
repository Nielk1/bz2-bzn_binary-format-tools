using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "torpedo")]
    public class ClassTorpedo2Factory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTorpedo2(PrjID, isUser, classLabel);
            ClassTorpedo2.Hydrate(parent, reader, obj as ClassTorpedo2);
            return true;
        }
    }
    public class ClassTorpedo2 : ClassGameObject
    {
        public ClassTorpedo2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassTorpedo2? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("lifeTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse lifeTimer/FLOAT");
            float lifeTimer = tok.GetSingle();

            ClassGameObject.Hydrate(parent, reader, obj as ClassGameObject);
        }
    }
}
