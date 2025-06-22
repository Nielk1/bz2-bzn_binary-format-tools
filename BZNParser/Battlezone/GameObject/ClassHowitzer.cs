﻿using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "howitzer")]
    [ObjectClass(BZNFormat.BattlezoneN64, "howitzer")]
    public class ClassHowitzerFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassHowitzer(PrjID, isUser, classLabel);
            ClassHowitzer.Hydrate(parent, reader, obj as ClassHowitzer);
            return true;
        }
    }
    public class ClassHowitzer : ClassTurretTank1
    {
        public ClassHowitzer(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassHowitzer? obj)
        {
            if (reader.Format == BZNFormat.Battlezone && reader.Version < 1020)
            {
                ClassHoverCraft.Hydrate(parent, reader, obj as ClassHoverCraft);
                return;
            }
            ClassTurretTank1.Hydrate(parent, reader, obj as ClassTurretTank1);
        }
    }
}
