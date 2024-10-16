using System;

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

static class EnumExtension
{
    // Enum‚²‚Æ‚ÉŠg’£ƒƒ\ƒbƒh‚ðì‚é
    public static bool HasBitFlag(this CHARA_TYPE value, CHARA_TYPE flag) => (value & flag) == flag;
}