﻿using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassTurretTank : ClassHoverCraft
    {
        public float undeffloat1 { get; set; }
        public float undeffloat2 { get; set; }
        public float undeffloat3 { get; set; }
        public float undeffloat4 { get; set; }
        public UInt32 undefraw { get; set; }
        public float undeffloat5 { get; set; }
        public bool undefbool { get; set; }

        public ClassTurretTank() { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat1 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat2 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat3 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat4 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefraw", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse undefraw/VOID");
            undefraw = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat5 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefbool", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse undefbool/BOOL");
            undefbool = tok.GetBoolean();

            base.LoadData(reader);
        }
    }
}
