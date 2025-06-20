using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "bomberbay")]
    public class ClassBomberBayFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassBomberBay(PrjID, isUser, classLabel);
            ClassBomberBay.Hydrate(reader, obj as ClassBomberBay);
            return true;
        }
    }
    public class ClassBomberBay : ClassPoweredBuilding
    {
        public ClassBomberBay(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassBomberBay? obj)
        {
            IBZNToken tok;

            if (reader.Version >= 1131)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("Handle", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse Handle/LONG");
                //int buildCount = tok.GetInt32();
                // m_MyBomber
            }
            else
            {
                // find bomber via slot scan
            }

            ClassPoweredBuilding.Hydrate(reader, obj as ClassPoweredBuilding);
        }
    }
}
