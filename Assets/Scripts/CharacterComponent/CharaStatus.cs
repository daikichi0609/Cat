using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStatus : MonoBehaviour
{
    private ObjectHolder ObjectHolder { get; set; }

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
    private int CurrentHp { get; set; }

    private void Start()
    {
        ObjectHolder = GetComponent<ObjectHolder>();
    }

    /// <summary>
    /// ダメージ
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>死亡判定</returns>
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
        var self = ObjectHolder.MoveObject;
        ObjectPoolManager.GetInstance().SetGameObject(m_CharaName, self);
    }
}
