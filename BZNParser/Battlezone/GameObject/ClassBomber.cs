using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "bomber")]
    public class ClassBomberFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBomber(PrjID, isUser, classLabel);
            ClassBomber.Hydrate(reader, obj as ClassBomber);
            return true;
        }
    }
    public class ClassBomber : ClassHoverCraft
    {
        public ClassBomber(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassBomber? obj)
        {
            // if reader.SaveType != 0
            // m_ReloadTime

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            UInt32 state = tok.GetUInt32H();

            // if reader.SaveType != 0
            // reloadTime float

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
