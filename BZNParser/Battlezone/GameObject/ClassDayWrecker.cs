using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "daywrecker")]
    [ObjectClass(BZNFormat.BattlezoneN64, "daywrecker")]
    [ObjectClass(BZNFormat.Battlezone2, "daywrecker")]
    public class ClassDayWreckerFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassDayWrecker(PrjID, isUser, classLabel);
            ClassDayWrecker.Hydrate(reader, obj as ClassDayWrecker);
            return true;
        }
    }
    public class ClassDayWrecker : ClassPowerUp
    {
        public ClassDayWrecker(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassDayWrecker? obj)
        {
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
