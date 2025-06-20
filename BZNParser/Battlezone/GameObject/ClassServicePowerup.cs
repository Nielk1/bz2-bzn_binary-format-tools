using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "servicepod")]
    [ObjectClass(BZNFormat.Battlezone2, "ammopack")]
    [ObjectClass(BZNFormat.Battlezone2, "repairkit")]
    public class ClassServicePowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassServicePowerup(PrjID, isUser, classLabel);
            ClassServicePowerup.Build(reader, obj as ClassServicePowerup);
            return true;
        }
    }
    public class ClassServicePowerup : ClassPowerUp
    {
        public ClassServicePowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassServicePowerup? obj)
        {
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
        }
    }
}
