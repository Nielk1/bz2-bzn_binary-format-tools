using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "walker")]
    [ObjectClass(BZNFormat.BattlezoneN64, "walker")]
    public class ClassWalker1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassWalker1(PrjID, isUser, classLabel);
            ClassWalker1.Hydrate(reader, obj as ClassWalker1);
            return true;
        }
    }
    public class ClassWalker1 : ClassCraft
    {
        public ClassWalker1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassWalker1? obj)
        {
            if (reader.Version > 1001 && reader.Version < 1026)
            {
                // junk hovercraft params
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
                reader.ReadToken();
            }

            ClassCraft.Hydrate(reader, obj as ClassCraft);
        }
    }
}
