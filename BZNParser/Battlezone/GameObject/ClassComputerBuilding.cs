using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "computer")]
    public class ClassComputerBuilding : ClassDummy
    {
        public string name { get; set; }
        public ClassComputerBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);

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
    [ObjectClass(BZNFormat.Battlezone2, "cnozzle")]
    public class ClassNozzelBuilding: ClassBuilding
    {
        public ClassNozzelBuilding(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
