using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "moneybag")]
    public class ClassMoneyPowerupFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMoneyPowerup(preamble, classLabel);
            ClassMoneyPowerup.Hydrate(parent, reader, obj as ClassMoneyPowerup);
            return true;
        }
    }
    public class ClassMoneyPowerup : ClassPowerUp
    {
        public ClassMoneyPowerup(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassMoneyPowerup? obj)
        {
            ClassPowerUp.Hydrate(parent, reader, obj as ClassPowerUp);
        }
    }
}
