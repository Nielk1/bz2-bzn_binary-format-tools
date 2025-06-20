using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "artillery")]
    public class ClassArtilleryFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassArtillery(PrjID, isUser, classLabel);
            ClassArtillery.Hydrate(reader, obj as ClassArtillery);
            return true;
        }
    }
    public class ClassArtillery : ClassTurretTank2
    {
        public ClassArtillery(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassArtillery? obj)
        {
            if (reader.Version < 1110)
            {
                IBZNToken tok;

                tok = reader.ReadToken();
                if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
                UInt32 state = tok.GetUInt32H();

                // block of reader.SaveType != 0

                ClassHoverCraft.Hydrate(reader, obj as ClassHoverCraft);
                return;
            }
            else
            {
                // block of reader.SaveType != 0
            }

            ClassTurretTank2.Hydrate(reader, obj as ClassTurretTank2);
        }
    }
}
