using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "servicepod")]
    [ObjectClass(BZNFormat.Battlezone2, "ammopack")]
    [ObjectClass(BZNFormat.Battlezone2, "repairkit")]
    public class ClassServicePowerupFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassServicePowerup(preamble, classLabel);
            ClassServicePowerup.Hydrate(parent, reader, obj as ClassServicePowerup);
            return true;
        }
    }
    public class ClassServicePowerup : ClassPowerUp
    {
        public ClassServicePowerup(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassServicePowerup? obj)
        {
            ClassPowerUp.Hydrate(parent, reader, obj as ClassPowerUp);
        }
    }
}
