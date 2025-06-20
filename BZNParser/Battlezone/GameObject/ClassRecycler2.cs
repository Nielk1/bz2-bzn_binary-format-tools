using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "recycler")]
    public class ClassRecycler2Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassRecycler2(PrjID, isUser, classLabel);
            ClassRecycler2.Hydrate(reader, obj as ClassRecycler2);
            return true;
        }
    }
    public class ClassRecycler2 : ClassFactory2
    {
        public UInt32 undefptr { get; set; }

        public ClassRecycler2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassRecycler2? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scrapTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scrapTimer/FLOAT");
            //scrapTimer = tok.GetSingle();

            ClassFactory2.Hydrate(reader, obj as ClassFactory2);
        }
    }
}
