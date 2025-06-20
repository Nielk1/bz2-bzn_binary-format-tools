﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{

    [ObjectClass(BZNFormat.Battlezone, "ammopack")]
    [ObjectClass(BZNFormat.BattlezoneN64, "ammopack")]
    public class ClassAmmoPowerupFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassAmmoPowerup(PrjID, isUser, classLabel);
            ClassAmmoPowerup.Hydrate(reader, obj as ClassAmmoPowerup);
            return true;
        }
    }
    public class ClassAmmoPowerup : ClassPowerUp
    {
        public ClassAmmoPowerup(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassAmmoPowerup? obj)
        {
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
