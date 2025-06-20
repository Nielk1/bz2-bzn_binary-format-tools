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
    public class ClassWeaponPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassWeaponPowerup(PrjID, isUser, classLabel);
            ClassWeaponPowerup.Build(reader, obj as ClassWeaponPowerup);
            return true;
        }
    }
    public class ClassWeaponPowerup : ClassPowerUp
    {
        public ClassWeaponPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassWeaponPowerup? obj)
        {
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
        }
    }
}
