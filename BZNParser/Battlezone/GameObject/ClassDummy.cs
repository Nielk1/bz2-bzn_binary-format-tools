using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "terrain")]
    public class ClassDummy : ClassGameObject
    {
        public string name { get; set; }
        public ClassDummy(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }

        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Version == 1047)
            {
                base.LoadData(reader); // this might be due to a changed base class on "spawnpnt"
                return;
            }

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                throw new Exception("Failed to parse name/CHAR");
            name = tok.GetString();

            // Terrain doesn't call base data load
            //base.LoadData(reader);
        }
    }
}
