using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassPowerUp : ClassGameObject
    {
        public ClassPowerUp(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }

        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
