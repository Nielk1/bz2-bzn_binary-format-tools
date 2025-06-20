using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "spawnpnt")]
    [ObjectClass(BZNFormat.BattlezoneN64, "spawnpnt")]
    public class ClassSpawnBuoy1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassSpawnBuoy1(PrjID, isUser, classLabel);
            ClassSpawnBuoy1.Hydrate(reader, obj as ClassSpawnBuoy1);
            return true;
        }
    }
    public class ClassSpawnBuoy1 : ClassGameObject
    {
        public ClassSpawnBuoy1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassSpawnBuoy1? obj)
        {
            ClassGameObject.Hydrate(reader, obj as ClassGameObject);
        }
    }
}
