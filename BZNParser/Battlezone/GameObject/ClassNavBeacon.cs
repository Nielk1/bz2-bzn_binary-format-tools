using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "beacon")]
    public class ClassNavBeaconFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassNavBeacon(PrjID, isUser, classLabel);
            ClassNavBeacon.Hydrate(reader, obj as ClassNavBeacon);
            return true;
        }
    }
    public class ClassNavBeacon : ClassGameObject
    {
        public ClassNavBeacon(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassNavBeacon? obj)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                throw new Exception("Failed to parse name/CHAR");
            string name = tok.GetString();

            tok = reader.ReadToken();
            if (!tok.Validate("navSlot", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse navSlot/LONG");
            //int navSlot = tok.GetInt32();

            if (reader.Version > 1104)
            {
                ClassGameObject.Hydrate(reader, obj as ClassGameObject);
            }
        }
    }
}
