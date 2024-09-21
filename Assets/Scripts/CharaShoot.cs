using System;
using UniRx;
using UnityEngine;

public class CharaShoot : MonoBehaviour
{
    /// <summary>
    /// スピード
    /// </summary>
    private static readonly float ms_Speed = 3f;

    [SerializeField]
    private InputManager m_InputManager;

    private void Awake()
    {
        // 入力購読
        m_InputManager.InputEvent.SubscribeWithState(this, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);
    }

    /// <summary>
    /// 購読用
    /// </summary>
    /// <param name="flag"></param>
    private void DetectInput(KeyCodeFlag flag)
    {
        // 移動検知
        if (DetectInputShoot(flag) == true)
            return;
    }

    /// <summary>
    /// 移動入力検知
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    private bool DetectInputShoot(KeyCodeFlag flag)
    {
        if (flag.HasBitFlag(KeyCodeFlag.Mouse0))
        {
            Shoot();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="dir"></param>
    private void Shoot()
    {

    }
}