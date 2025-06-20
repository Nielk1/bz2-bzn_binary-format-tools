using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "objectspawn")]
    public class ClassObjectSpawnFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassObjectSpawn(PrjID, isUser, classLabel);
            ClassObjectSpawn.Build(reader, obj as ClassObjectSpawn);
            return true;
        }
    }
    public class ClassObjectSpawn : ClassBuilding
    {
        public ClassObjectSpawn(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassObjectSpawn? obj)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("spawnHandle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse spawnHandle/LONG"); // type not confirmed
                //state = tok.GetUInt32();

                tok = reader.ReadToken();
                if (!tok.Validate("spawnTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse spawnTimer/FLOAT"); // type not confirmed
                //state = tok.GetSingle();
            }

            ClassBuilding.Build(reader, obj as ClassBuilding);
        }
    }
}
