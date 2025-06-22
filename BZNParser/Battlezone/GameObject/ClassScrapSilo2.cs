using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "silo")]
    public class ClassScrapSilo2Factory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScrapSilo2(PrjID, isUser, classLabel);
            ClassScrapSilo2.Hydrate(parent, reader, obj as ClassScrapSilo2);
            return true;
        }
    }
    public class ClassScrapSilo2 : ClassBuilding
    {
        public UInt32 undefptr { get; set; }

        public ClassScrapSilo2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassScrapSilo2? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("scrapTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse scrapTimer/FLOAT");
            //saveClass = tok.GetSingle();

            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
