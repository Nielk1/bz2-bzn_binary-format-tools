using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "walker")]
    [ObjectClass(BZNFormat.BattlezoneN64, "walker")]
    public class ClassWalker1 : ClassCraft
    {
        public ClassWalker1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
