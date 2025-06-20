using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "aircraft")]
    public class ClassAirCraftFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAirCraft(PrjID, isUser, classLabel);
            ClassAirCraft.Build(reader, obj as ClassAirCraft);
            return true;
        }
    }
    public class ClassAirCraft : ClassCraft
    {
        public ClassAirCraft(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassAirCraft? obj)
        {
            //if (reader.SaveType != 0)

            ClassCraft.Build(reader, obj as ClassCraft);
        }
    }
}
