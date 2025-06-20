using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "flare")]
    [ObjectClass(BZNFormat.BattlezoneN64, "flare")]
    [ObjectClass(BZNFormat.Battlezone2, "flare")]
    public class ClassFlareMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassFlareMine(PrjID, isUser, classLabel);
            ClassFlareMine.Build(reader, obj as ClassFlareMine);
            return true;
        }
    }
    public class ClassFlareMine : ClassMine
    {
        public ClassFlareMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassFlareMine? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //saveClass = tok.GetSingle();
                //shotTimer

                //tok = reader.ReadToken();
                //if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
                //saveClass = tok.GetSingle();
            }

            ClassMine.Build(reader, obj as ClassMine);
        }
    }
}
