using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "magnet")]
    [ObjectClass(BZNFormat.BattlezoneN64, "magnet")]
    [ObjectClass(BZNFormat.Battlezone2, "magnet")]
    public class ClassMagnetMineFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassMagnetMine(PrjID, isUser, classLabel);
            ClassMagnetMine.Build(reader, obj as ClassMagnetMine);
            return true;
        }
    }
    public class ClassMagnetMine : ClassMine
    {
        public ClassMagnetMine(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassMagnetMine? obj)
        {
            IBZNToken tok;

            //if (reader.Format == BZNFormat.Battlezone2)
            //{
            //    tok = reader.ReadToken();
            //    if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            //    //saveClass = tok.GetSingle();
            //}

            ClassMine.Build(reader, obj as ClassMine);
        }
    }
}
