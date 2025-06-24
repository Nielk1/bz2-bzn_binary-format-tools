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
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, BZNGameObjectWrapper preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassWeaponPowerup(preamble, classLabel);
            ClassWeaponPowerup.Hydrate(parent, reader, obj as ClassWeaponPowerup);
            return true;
        }
    }
    public class ClassWeaponPowerup : ClassPowerUp
    {
        public ClassWeaponPowerup(BZNGameObjectWrapper preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassWeaponPowerup? obj)
        {
            ClassPowerUp.Hydrate(parent, reader, obj as ClassPowerUp);
        }
    }
}
