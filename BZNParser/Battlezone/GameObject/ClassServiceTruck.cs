using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "service")]
    public class ClassServiceTruckFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassServiceTruck(PrjID, isUser, classLabel);
            ClassServiceTruck.Build(reader, obj as ClassServiceTruck);
            return true;
        }
    }
    public class ClassServiceTruck : ClassTrackedVehicle
    {
        public ClassServiceTruck(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassServiceTruck? obj)
        {
            ClassTrackedVehicle.Build(reader, obj as ClassTrackedVehicle);
        }
    }
}
