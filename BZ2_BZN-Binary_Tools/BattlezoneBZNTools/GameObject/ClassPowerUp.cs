﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassPowerUp : ClassGameObject
    {
        public ClassPowerUp(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }

        public override string GetBZ1ASCII()
        {
            return base.GetBZ1ASCII();
        }
    }
}
