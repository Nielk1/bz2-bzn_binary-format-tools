using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "weaponmine")]
    [ObjectClass(BZNFormat.BattlezoneN64, "weaponmine")]
    [ObjectClass(BZNFormat.Battlezone2, "weaponmine")]
    public class ClassWeaponMine : ClassMine
    {
        public ClassWeaponMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version >= 1144)
                {
                    IBZNToken tok;

                    tok = reader.ReadToken();
                    if (!tok.Validate("curAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse curAmmo/FLOAT");
                    curAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("maxAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse maxAmmo/FLOAT");
                    maxAmmo = (int)tok.GetSingle();

                    tok = reader.ReadToken();
                    if (!tok.Validate("addAmmo", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse addAmmo/FLOAT");
                    float addAmmo = (uint)tok.GetSingle();
                }
            }

            base.LoadData(reader);
        }
    }
}
