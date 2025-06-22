using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "computer")]
    public class ClassComputerBuildingFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassComputerBuilding(PrjID, isUser, classLabel);
            ClassComputerBuilding.Hydrate(parent, reader, obj as ClassComputerBuilding);
            return true;
        }
    }
    public class ClassComputerBuilding : ClassDummy
    {
        public string name { get; set; }
        public ClassComputerBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassComputerBuilding? obj)
        {
            ClassDummy.Hydrate(parent, reader, obj as ClassDummy);

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("Nozzle1", BinaryFieldType.DATA_VOID))
                throw new Exception("Failed to parse Nozzle1/VOID");
            //tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("Nozzle2", BinaryFieldType.DATA_VOID))
                throw new Exception("Failed to parse Nozzle2/VOID");
            //tok.GetUInt32H();
        }
    }
}
