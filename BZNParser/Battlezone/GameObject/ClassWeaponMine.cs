using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "weaponmine")]
    [ObjectClass(BZNFormat.BattlezoneN64, "weaponmine")]
    [ObjectClass(BZNFormat.Battlezone2, "weaponmine")]
    public class ClassWeaponMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassWeaponMine(PrjID, isUser, classLabel);
            ClassWeaponMine.Hydrate(reader, obj as ClassWeaponMine);
            return true;
        }
    }
    public class ClassWeaponMine : ClassMine
    {
        public ClassWeaponMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassWeaponMine? obj)
        {
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1144)
                {
                    IBZNToken tok;

                    tok = reader.ReadToken();
                    if (!tok.Validate("curAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse curAmmo/FLOAT");
                    if (obj != null) obj.curAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("maxAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse maxAmmo/FLOAT");
                    if (obj != null) obj.maxAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("addAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse addAmmo/FLOAT");
                    float addAmmo = (uint)tok.GetSingle();
                }
            }

            ClassMine.Hydrate(reader, obj as ClassMine);
        }
    }
}
