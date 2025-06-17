using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrap")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrap")]
    [ObjectClass(BZNFormat.Battlezone2, "scrap")]
    public class ClassScrap : ClassGameObject
    {
        public ClassScrap(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            //if (a2->isSave)
            //{
            //    (a2->vftable->field_38)(a2, &this[1].gap8[52], 4, "expireTime");
            //    (a2->vftable->out_bool)(a2, &this[1].gap8[48], 1, "HardToGetTo");
            //}
            base.LoadData(reader);
        }
    }
    
}
