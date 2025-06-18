using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "powerup")]
    public class ClassPowerUp : ClassGameObject
    {
        public ClassPowerUp(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (reader.Version < 1031)
                {
                    if (ClassLabel == "torpedo")
                    {
                        base.LoadData(reader);
                        return;
                    }
                }
            }

            if (reader.Format == BZNFormat.Battlezone && reader.SaveType != 0)
            {
                // flags
            }
            base.LoadData(reader);
        }
    }
}
