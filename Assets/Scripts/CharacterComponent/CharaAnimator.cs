using UnityEngine;
using UniRx;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NaughtyAttributes;

/// <summary>
/// アニメーションパターン定義
/// </summary>
public enum ANIMATION_TYPE
{
    /// <summary>
    /// 通常
    /// </summary>
    IDLE,

    /// <summary>
    /// 通常2
    /// </summary>
    IDLE_2,

    /// <summary>
    /// 移動
    /// </summary>
    MOVE,

    /// <summary>
    /// 攻撃
    /// </summary>
    ATTACK,

    /// <summary>
    /// 被ダメージ
    /// </summary>
    DAMAGE,

    /// <summary>
    /// 眠り
    /// </summary>
    SLEEP,

    /// <summary>
    /// スキル
    /// </summary>
    SKILL,
}

public class CharaAnimator : MonoBehaviour
{
    [Serializable]
    private struct ActTicket { }

    /// <summary>
    /// キャラが持つアニメーター
    /// </summary>
    [SerializeField]
    private Animator m_CharaAnimator;

    /// <summary>
    /// 行動中ステータス
    /// </summary>
    private Queue<ActTicket> m_TicketHolder = new Queue<ActTicket>();
    [ShowNativeProperty]
    public bool IsActing => m_TicketHolder.Count != 0;

    /// <summary>
    /// Act登録
    /// </summary>
    /// <returns></returns>
    private IDisposable RegisterAct()
    {
        var t = new ActTicket();
        m_TicketHolder.Enqueue(t);
        return Disposable.CreateWithState(m_TicketHolder, self => self.Dequeue());
    }

    /// <summary>
    /// アニメーション
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IDisposable PlayAnimation(ANIMATION_TYPE type)
    {
        var key = GetKey(type);
        m_CharaAnimator.SetBool(key, true);
        return Disposable.CreateWithState((this, key), tuple => tuple.Item1.m_CharaAnimator.SetBool(tuple.key, false));
    }

    /// <summary>
    /// 非同期アニメーション
    /// </summary>
    /// <param name="type"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    async Task PlayAnimation(ANIMATION_TYPE type, float time)
    {
        var acting = RegisterAct();
        var animation = PlayAnimation(type);

        await Task.Delay((int)(time * 1000));
        acting.Dispose();
        animation.Dispose();
    }

    /// <summary>
    /// アニメーション切り替えキー取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetKey(ANIMATION_TYPE type)
    {
        string key = type switch
        {
            ANIMATION_TYPE.IDLE => "",
            ANIMATION_TYPE.MOVE => "IsRunning",
            ANIMATION_TYPE.ATTACK => "IsAttacking",
            ANIMATION_TYPE.DAMAGE => "IsDamaging",
            ANIMATION_TYPE.SLEEP => "IsSleeping",
            ANIMATION_TYPE.SKILL => "UsingSkill",
            _ => "",
        };

        return key;
    }
}