﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public class BZNValidationToken : IBZNToken
    {
        private string name;

        public BZNValidationToken(string name)
        {
            this.name = name;
        }

        public bool GetBoolean(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Int32 GetInt32(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt32 GetUInt32(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt32 GetUInt32H(int index = 0) { return GetUInt32(); }
        public Int16 GetInt16(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public UInt16 GetUInt16(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public float GetSingle(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public string GetString(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Vector3D GetVector3D(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Vector2D GetVector2D(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Matrix GetMatrix(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public Euler GetEuler(int index = 0) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }
        public byte[] GetRaw(int index = 0, int length = 1) { throw new InvalidOperationException("Validation Tokens have no data, check IsValidationOnly() before processing"); }

        public bool IsValidationOnly() { return true; }

        public override string ToString()
        {
            return "VALDT\t[" + name + "]";
        }

        public bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN)
        {
            return this.name == name;
        }
    }
}
