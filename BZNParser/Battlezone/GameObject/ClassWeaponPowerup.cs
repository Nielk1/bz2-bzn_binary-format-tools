using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "wpnpower")]
    [ObjectClass(BZNFormat.BattlezoneN64, "wpnpower")]
    [ObjectClass(BZNFormat.Battlezone2, "wpnpower")]
    public class ClassWeaponPowerup : ClassPowerUp
    {
        public ClassWeaponPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }
    }
}
