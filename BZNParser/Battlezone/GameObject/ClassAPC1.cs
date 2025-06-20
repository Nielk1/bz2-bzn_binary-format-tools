using BZNParser.Reader;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "apc")]
    [ObjectClass(BZNFormat.BattlezoneN64, "apc")]
    public class ClassAPC1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAPC1(PrjID, isUser, classLabel);
            ClassAPC1.Hydrate(reader, obj as ClassAPC1);
            return true;
        }
    }
    public class ClassAPC1 : ClassHoverCraft
    {
        public UInt32 soldierCount { get; set; }
        public byte[] state { get; set; }
        public ClassAPC1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassAPC1? obj)
        {
            IBZNToken tok;
            
            tok = reader.ReadToken();
            if (!tok.Validate("soldierCount", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse soldierCount/LONG");
            if (obj != null) obj.soldierCount = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID))
                throw new Exception("Failed to parse state/VOID");
            if (obj != null) obj.state = tok.GetBytes(0, 4);

            ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
        }
    }
}
