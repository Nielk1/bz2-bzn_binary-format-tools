using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "camerapod")]
    [ObjectClass(BZNFormat.BattlezoneN64, "camerapod")]
    [ObjectClass(BZNFormat.Battlezone2, "camerapod")]
    public class ClassCameraPodFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassCameraPod(PrjID, isUser, classLabel);
            ClassCameraPod.Hydrate(reader, obj as ClassCameraPod);
            return true;
        }
    }
    public class ClassCameraPod : ClassPowerUp
    {
        public ClassCameraPod(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassCameraPod? obj)
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

            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
