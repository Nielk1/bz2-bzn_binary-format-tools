using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "repairkit")]
    [ObjectClass(BZNFormat.BattlezoneN64, "repairkit")]
    public class ClassHealthPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassHealthPowerup(PrjID, isUser, classLabel);
            ClassHealthPowerup.Build(reader, obj as ClassHealthPowerup);
            return true;
        }
    }
    public class ClassHealthPowerup : ClassPowerUp
    {
        public ClassHealthPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassHealthPowerup? obj)
        {
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
        }
    }
}
