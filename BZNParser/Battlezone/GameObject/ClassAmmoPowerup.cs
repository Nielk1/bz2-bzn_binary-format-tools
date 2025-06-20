using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{

    [ObjectClass(BZNFormat.Battlezone, "ammopack")]
    [ObjectClass(BZNFormat.BattlezoneN64, "ammopack")]
    public class ClassAmmoPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAmmoPowerup(PrjID, isUser, classLabel);
            ClassAmmoPowerup.Build(reader, obj as ClassAmmoPowerup);
            return true;
        }
    }
    public class ClassAmmoPowerup : ClassPowerUp
    {
        public ClassAmmoPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassAmmoPowerup? obj)
        {
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
        }
    }
}
