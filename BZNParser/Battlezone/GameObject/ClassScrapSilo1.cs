using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "scrapsilo")]
    [ObjectClass(BZNFormat.BattlezoneN64, "scrapsilo")]
    public class ClassScrapSilo1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassScrapSilo1(PrjID, isUser, classLabel);
            ClassScrapSilo1.Build(reader, obj as ClassScrapSilo1);
            return true;
        }
    }
    public class ClassScrapSilo1 : ClassGameObject
    {
        public UInt32 undefptr { get; set; }

        public ClassScrapSilo1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassScrapSilo1? obj)
        {
            if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version > 1020)
            {
                IBZNToken tok = reader.ReadToken();
                if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/LONG");
                if (obj != null) obj.undefptr = tok.GetUInt32H(); // dropoff
            }

            ClassGameObject.Build(reader, obj as ClassGameObject);
        }
    }
}
