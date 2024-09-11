using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Zenject;

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

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// 入力イベント
    /// </summary>
    private Subject<InputInfo> m_InputEvent = new Subject<InputInfo>();
    public IObservable<InputInfo> InputEvent => m_InputEvent;
    private KeyCodeFlag m_InputKeyCode;
    public KeyCodeFlag InputKeyCode => m_InputKeyCode;

    /// <summary>
    /// 入力始めイベント
    /// </summary>
    private Subject<InputInfo> m_InputStartEvent = new Subject<InputInfo>();
    public IObservable<InputInfo> InputStartEvent => m_InputStartEvent;
    private KeyCodeFlag m_InputStartKeyCode;
    public KeyCodeFlag InputStartKeyCode => m_InputStartKeyCode;

    private void Update()
    {
        DetectInput();
    }

    /// <summary>
    /// 入力を見てメッセージ発行
    /// </summary>
    private void DetectInput()
    {
        m_InputKeyCode = CreateGetKeyFlag();
        m_InputStartKeyCode = CreateGetKeyDownFlag();

        m_InputEvent.OnNext(new InputInfo(m_InputKeyCode));
        m_InputStartEvent.OnNext(new InputInfo(m_InputStartKeyCode));
    }

    /// <summary>
    /// 入力中ずっと
    /// </summary>
    /// <returns></returns>
	private KeyCodeFlag CreateGetKeyFlag()
    {
        var flag = KeyCodeFlag.None;

        if (Input.GetKey(KeyCode.W))
            flag |= KeyCodeFlag.W;

        if (Input.GetKey(KeyCode.A))
            flag |= KeyCodeFlag.A;

        if (Input.GetKey(KeyCode.S))
            flag |= KeyCodeFlag.S;

        if (Input.GetKey(KeyCode.D))
            flag |= KeyCodeFlag.D;

        if (Input.GetKey(KeyCode.RightShift))
            flag |= KeyCodeFlag.Right_Shift;

        if (Input.GetKey(KeyCode.E))
            flag |= KeyCodeFlag.E;

        if (Input.GetKey(KeyCode.Q))
            flag |= KeyCodeFlag.Q;

        if (Input.GetKey(KeyCode.M))
            flag |= KeyCodeFlag.M;

        if (Input.GetKey(KeyCode.Return))
            flag |= KeyCodeFlag.Return;

        if (Input.GetKey(KeyCode.Alpha1))
            flag |= KeyCodeFlag.One;

        if (Input.GetKey(KeyCode.Alpha2))
            flag |= KeyCodeFlag.Two;

        if (Input.GetKey(KeyCode.Alpha3))
            flag |= KeyCodeFlag.Three;

        return flag;
    }

    /// <summary>
    /// 入力時
    /// </summary>
    /// <returns></returns>
    private KeyCodeFlag CreateGetKeyDownFlag()
    {
        var flag = KeyCodeFlag.None;

        if (Input.GetKeyDown(KeyCode.W))
            flag |= KeyCodeFlag.W;

        if (Input.GetKeyDown(KeyCode.A))
            flag |= KeyCodeFlag.A;

        if (Input.GetKeyDown(KeyCode.S))
            flag |= KeyCodeFlag.S;

        if (Input.GetKeyDown(KeyCode.D))
            flag |= KeyCodeFlag.D;

        if (Input.GetKeyDown(KeyCode.RightShift))
            flag |= KeyCodeFlag.Right_Shift;

        if (Input.GetKeyDown(KeyCode.E))
            flag |= KeyCodeFlag.E;

        if (Input.GetKeyDown(KeyCode.Q))
            flag |= KeyCodeFlag.Q;

        if (Input.GetKeyDown(KeyCode.M))
            flag |= KeyCodeFlag.M;

        if (Input.GetKeyDown(KeyCode.Return))
            flag |= KeyCodeFlag.Return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            flag |= KeyCodeFlag.One;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            flag |= KeyCodeFlag.Two;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            flag |= KeyCodeFlag.Three;

        return flag;
    }
}

// 拡張メソッド
static class EnumExtensions
{
    public static bool HasBitFlag(this KeyCodeFlag value, KeyCodeFlag flag) => (value & flag) == flag;
}