using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "powerup")]
    public class ClassPowerUpFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassPowerUp(PrjID, isUser, classLabel);
            ClassPowerUp.Build(reader, obj as ClassPowerUp);
            return true;
        }
    }
    public class ClassPowerUp : ClassGameObject
    {
        public ClassPowerUp(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassPowerUp? obj)
        {
            if (reader.Format == BZNFormat.Battlezone && reader.SaveType != 0)
            {
                // flags
            }
            ClassGameObject.Build(reader, obj as ClassGameObject);
        }
    }
}
