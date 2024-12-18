﻿using System;

public enum CHARA_NAME
{
    BOXMAN = 0,
    ENEMY = 1,
}

[Flags]
public enum CHARA_TYPE
{
    NONE = 0,
    ENEMY = 1 << 0,
    ALLY = 1 << 1,
    PLAYER = 1 << 2,
}

[Flags]
public enum ATTACK_TYPE
{
    NONE = 0,
    CRUSH = 1 << 0,
}

static class EnumExtension
{
    // Enumごとに拡張メソッドを作る
    public static bool HasBitFlag(this CHARA_TYPE value, CHARA_TYPE flag) => (value & flag) == flag;

    public static bool HasBitFlag(this ATTACK_TYPE value, ATTACK_TYPE flag) => (value & flag) == flag;
}