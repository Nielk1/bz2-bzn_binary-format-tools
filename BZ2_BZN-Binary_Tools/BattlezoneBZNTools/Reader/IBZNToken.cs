﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.Reader
{
    public interface IBZNToken
    {
        bool GetBoolean(int index = 0);
        Int32 GetInt32(int index = 0);
        UInt32 GetUInt32(int index = 0);
        UInt32 GetUInt32H(int index = 0);
        Int16 GetInt16(int index = 0);
        UInt16 GetUInt16(int index = 0);
        string GetString(int index = 0);
        float GetSingle(int index = 0);
        Vector3D GetVector3D(int index = 0);
        Vector2D GetVector2D(int index = 0);
        Matrix GetMatrix(int index = 0);
        Euler GetEuler(int index = 0);
        byte[] GetRaw(int index = 0, int length = 1);

        bool IsValidationOnly();
        bool Validate(string name, BinaryFieldType type = BinaryFieldType.DATA_UNKNOWN);
    }
}
