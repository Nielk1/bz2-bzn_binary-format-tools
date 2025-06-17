using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassHoverCraft : ClassCraft
    {
        public ClassHoverCraft(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Format == BZNFormat.Battlezone && reader.Version > 1001 && reader.Version < 1026)
            {
                IBZNToken tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
                tok = reader.ReadToken();
            }

            base.LoadData(reader);
        }
    }
}
