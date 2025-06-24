﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "computer")]
    public class ClassComputerBuildingFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassComputerBuilding(preamble, classLabel);
            ClassComputerBuilding.Hydrate(parent, reader, obj as ClassComputerBuilding);
            return true;
        }
    }
    public class ClassComputerBuilding : ClassDummy
    {
        protected UInt32 Nozzle1_Handle { get; set; }
        protected UInt32 Nozzle2_Handle { get; set; }
        public ClassComputerBuilding(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }

        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassComputerBuilding? obj)
        {
            ClassDummy.Hydrate(parent, reader, obj as ClassDummy);

            IBZNToken tok;

            if (reader.Version >= 1102)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("Nozzle1", BinaryFieldType.DATA_VOID))
                    throw new Exception("Failed to parse Nozzle1/VOID");
                if (obj != null) obj.Nozzle1_Handle = tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("Nozzle2", BinaryFieldType.DATA_VOID))
                    throw new Exception("Failed to parse Nozzle2/VOID");
                if (obj != null) obj.Nozzle2_Handle = tok.GetUInt32H();
            }
            else
            {
                // realy hackery here, but hopefully no BZNs exist with this
                if (obj != null)
                {
                    obj.Malformations.Add(Malformation.NOTIMPLEMENTED, "Nozzle1_Handle");
                    obj.Malformations.Add(Malformation.NOTIMPLEMENTED, "Nozzle2_Handle");
                }
            }
        }
    }
}
