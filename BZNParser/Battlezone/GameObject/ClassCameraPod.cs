using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "camerapod")]
    [ObjectClass(BZNFormat.BattlezoneN64, "camerapod")]
    [ObjectClass(BZNFormat.Battlezone2, "camerapod")]
    public class ClassCameraPodFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassCameraPod(preamble, classLabel);
            ClassCameraPod.Hydrate(parent, reader, obj as ClassCameraPod);
            return true;
        }
    }
    public class ClassCameraPod : ClassPowerUp
    {
        public ClassCameraPod(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassCameraPod? obj)
        {
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1148)
                {
                    IBZNToken tok;

                    //tok = reader.ReadToken();
                    //if (!tok.Validate("name", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                    //name = tok.GetString();

                    tok = reader.ReadToken();
                    if (!tok.Validate("navSlot", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse navSlot/LONG");
                    //int navSlot = tok.GetInt32();
                }
            }

            ClassPowerUp.Hydrate(parent, reader, obj as ClassPowerUp);
        }
    }
}
