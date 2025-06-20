using BZNParser.Reader;
using System.Numerics;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "torpedo")]
    [ObjectClass(BZNFormat.BattlezoneN64, "torpedo")]
    public class ClassTorpedo1Factory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassTorpedo1(PrjID, isUser, classLabel);
            ClassTorpedo1.Hydrate(reader, obj as ClassTorpedo1);
            return true;
        }
    }
    public class ClassTorpedo1 : ClassPowerUp
    {
        public ClassTorpedo1(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNStreamReader reader, ClassTorpedo1? obj)
        {
            if (reader.Version < 1031)
            {
                if (reader.Version < 1019)
                {
                    // obsolete
                    IBZNToken tok;

                    tok = reader.ReadToken();
                    tok = reader.ReadToken();
                    tok = reader.ReadToken();
                    tok = reader.ReadToken();
                    tok = reader.ReadToken();
                    tok = reader.ReadToken();

                    tok = reader.ReadToken();
                    if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D))
                        throw new Exception("Failed to parse ???/VEC3D");
                    // there are 6 vectors here, but we don't know what they are for and are probably able to be forgotten
                }
                else if (reader.Version > 1027)
                {
			        // read in abandoned flag
                    IBZNToken tok;
                    tok = reader.ReadToken();
                }
            }

            if (reader.Format == BZNFormat.Battlezone && reader.Version < 1031)
            {
                ClassGameObject.Hydrate(reader, obj as ClassGameObject);
                return;
            }
            ClassPowerUp.Hydrate(reader, obj as ClassPowerUp);
        }
    }
}
