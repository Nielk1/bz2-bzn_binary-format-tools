using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{

    [ObjectClass(BZNFormat.Battlezone, "ammopack")]
    [ObjectClass(BZNFormat.BattlezoneN64, "ammopack")]
    public class ClassAmmoPowerup1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAmmoPowerup1(PrjID, isUser, classLabel);
            ClassAmmoPowerup1.Hydrate(reader, obj as ClassAmmoPowerup1);
            return true;
        }
    }
    public class ClassAmmoPowerup1 : ClassPowerUp
    {
        public ClassAmmoPowerup1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassAmmoPowerup1? obj)
        {
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
