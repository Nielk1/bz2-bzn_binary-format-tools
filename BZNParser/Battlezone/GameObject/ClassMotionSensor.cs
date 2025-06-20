using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "sensor")]
    public class ClassMotionSensorFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMotionSensor(PrjID, isUser, classLabel);
            ClassMotionSensor.Hydrate(reader, obj as ClassMotionSensor);
            return true;
        }
    }
    public class ClassMotionSensor : ClassPoweredBuilding
    {
        public ClassMotionSensor(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassMotionSensor? obj)
        {
            ClassPoweredBuilding.Hydrate(reader, obj as ClassPoweredBuilding);
        }
    }
}
