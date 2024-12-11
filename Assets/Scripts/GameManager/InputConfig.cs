using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum KeyCodeFlag
{
    None = 0,

    // ----- 移動 ----- //
    W = 1 << 0,
    A = 1 << 1,
    S = 1 << 2,
    D = 1 << 3,
    Right_Shift = 1 << 4,

    // 攻撃
    E = 1 << 5,

    // 戻る
    Q = 1 << 6,

    // メニュー
    M = 1 << 7,

    // Ui決定
    Return = 1 << 8,

    // スキル
    One = 1 << 9,
    Two = 1 << 10,
    Three = 1 << 11,

    // マウス
    Mouse0 = 1 << 12,
    Mouse1 = 1 << 13,
}

public readonly struct InputInfo
{
    /// <summary>
    /// キーコード
    /// </summary>
    public KeyCodeFlag KeyCodeFlag { get; }

    public InputInfo(KeyCodeFlag flag)
    {
        KeyCodeFlag = flag;
    }
}