using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "artillery")]
    public class ClassArtilleryFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassArtillery(PrjID, isUser, classLabel);
            ClassArtillery.Hydrate(parent, reader, obj as ClassArtillery);
            return true;
        }
    }
    public class ClassArtillery : ClassTurretTank2
    {
        public ClassArtillery(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassArtillery? obj)
        {
            if (reader.Version < 1110)
            {
                IBZNToken tok;

                tok = reader.ReadToken();
                if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
                UInt32 state = tok.GetUInt32H();

                // block of parent.SaveType != SaveType.BZN

                ClassHoverCraft.Hydrate(parent, reader, obj as ClassHoverCraft);
                return;
            }
            else
            {
                // block of parent.SaveType != SaveType.BZN
            }

            ClassTurretTank2.Hydrate(parent, reader, obj as ClassTurretTank2);
        }
    }
}
