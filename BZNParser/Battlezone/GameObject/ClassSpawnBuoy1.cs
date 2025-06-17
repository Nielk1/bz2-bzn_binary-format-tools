using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spawnpnt")] // does this exist?
    [ObjectClass(BZNFormat.BattlezoneN64, "spawnpnt")] // does this exist?
    public class ClassSpawnBuoy1 : ClassGameObject
    {
        public ClassSpawnBuoy1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
