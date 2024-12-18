﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CharaStatus : ComponentBase
{
    private CharaObjectHolder ObjectHolder { get; set; }

    /// <summary>
    /// キャラ名
    /// </summary>
    [SerializeField]
    private CHARA_NAME m_CharaName;

    /// <summary>
    /// 味方か敵か
    /// </summary>
    [SerializeField]
    private CHARA_TYPE m_Type;
    public CHARA_TYPE Type => m_Type;

    /// <summary>
    /// 最大HP
    /// </summary>
    [SerializeField]
    private int m_MaxHp;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private ReactiveProperty<int> m_CurrentHp = new ReactiveProperty<int>();
    private IObservable<int> CurrentHpObservable => m_CurrentHp;
    private int CurrentHp { get => m_CurrentHp.Value; set => m_CurrentHp.Value = value; }

    /// <summary>
    /// Hpバー
    /// </summary>
    public Slider HpBar { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        ObjectHolder = GetComponent<CharaObjectHolder>();

        if (HpBar != null)
        {
            HpBar.maxValue = m_MaxHp;
            HpBar.minValue = 0;
            CurrentHp = m_MaxHp;
            // Hpバー更新
            CurrentHpObservable.SubscribeWithState(HpBar, (v, bar) =>
            {
                if (bar != null)
                    bar.value = v;
            }).AddTo(Owner.CompositeDisposable);
        }
    }

    /// <summary>
    /// ダメージ
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        CurrentHp = Math.Clamp(CurrentHp - damage, 0, m_MaxHp); // 0未満にならない
        if (CurrentHp <= 0)
            Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        Owner.Dispose();
    }
}
