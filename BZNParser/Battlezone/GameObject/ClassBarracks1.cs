﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "barracks")]
    [ObjectClass(BZNFormat.BattlezoneN64, "barracks")]
    public class ClassBarracks1Factory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBarracks1(preamble, classLabel);
            ClassBarracks1.Hydrate(parent, reader, obj as ClassBarracks1);
            return true;
        }
    }
    public class ClassBarracks1 : ClassBuilding
    {
        protected long nextEmptyCheck { get; set; }
        public ClassBarracks1(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassBarracks1? obj)
        {
            if (parent.SaveType != SaveType.BZN)
            {
                IBZNToken tok = reader.ReadToken();
                if (!tok.Validate("nextEmptyCheck", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse nextEmptyCheck/LONG");
                if (obj != null) obj.nextEmptyCheck = tok.GetInt32();
            }

            ClassBuilding.Hydrate(parent, reader, obj as ClassBuilding);
        }
    }
}
