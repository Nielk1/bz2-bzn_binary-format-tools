using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "moneybag")]
    public class ClassMoneyPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMoneyPowerup(PrjID, isUser, classLabel);
            ClassMoneyPowerup.Build(reader, obj as ClassMoneyPowerup);
            return true;
        }
    }
    public class ClassMoneyPowerup : ClassPowerUp
    {
        public ClassMoneyPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassMoneyPowerup? obj)
        {
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
        }
    }
}
