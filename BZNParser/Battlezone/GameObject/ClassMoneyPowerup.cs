using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "moneybag")]
    public class ClassMoneyPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMoneyPowerup(PrjID, isUser, classLabel);
            ClassMoneyPowerup.Hydrate(reader, obj as ClassMoneyPowerup);
            return true;
        }
    }
    public class ClassMoneyPowerup : ClassPowerUp
    {
        public ClassMoneyPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassMoneyPowerup? obj)
        {
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
