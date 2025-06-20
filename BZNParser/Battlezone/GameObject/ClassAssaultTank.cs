using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "assaulttank")]
    public class ClassAssaultTankFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAssaultTank(PrjID, isUser, classLabel);
            ClassAssaultTank.Build(reader, obj as ClassAssaultTank);
            return true;
        }
    }
    public class ClassAssaultTank : ClassTrackedVehicle
    {
        public ClassAssaultTank(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassAssaultTank? obj)
        {
            ClassTrackedVehicle.Build(reader, obj as ClassTrackedVehicle);
        }
    }
}
