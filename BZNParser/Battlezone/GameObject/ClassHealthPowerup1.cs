using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "repairkit")]
    [ObjectClass(BZNFormat.BattlezoneN64, "repairkit")]
    public class ClassHealthPowerup1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassHealthPowerup1(PrjID, isUser, classLabel);
            ClassHealthPowerup1.Hydrate(reader, obj as ClassHealthPowerup1);
            return true;
        }
    }
    public class ClassHealthPowerup1 : ClassPowerUp
    {
        public ClassHealthPowerup1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassHealthPowerup1? obj)
        {
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
