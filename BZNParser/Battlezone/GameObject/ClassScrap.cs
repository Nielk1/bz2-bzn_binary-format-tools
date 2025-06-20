using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrap")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrap")]
    [ObjectClass(BZNFormat.Battlezone2, "scrap")]
    public class ClassScrapFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScrap(PrjID, isUser, classLabel);
            ClassScrap.Hydrate(reader, obj as ClassScrap);
            return true;
        }
    }
    public class ClassScrap : ClassGameObject
    {
        public ClassScrap(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassScrap? obj)
        {
            //if (a2->isSave)
            //{
            //    (a2->vftable->field_38)(a2, &this[1].gap8[52], 4, "expireTime");
            //    (a2->vftable->out_bool)(a2, &this[1].gap8[48], 1, "HardToGetTo");
            //}
            ClassGameObject.Hydrate(reader, obj as ClassGameObject);
        }
    }
    
}
