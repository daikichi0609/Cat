using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStatus : MonoBehaviour
{
    public enum CHARA_TYPE
    {
        ENEMY,
        ALLY,
    }

    /// <summary>
    /// キャラ名
    /// </summary>
    [SerializeField]
    private CHARA_NAME m_Name;

    /// <summary>
    /// 味方か敵か
    /// </summary>
    [SerializeField]
    private CHARA_TYPE m_Type;

    /// <summary>
    /// 最大HP
    /// </summary>
    [SerializeField]
    private int m_MaxHp;

    [SerializeField]
    private ObjectHolder m_ObjectHolder;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private int CurrentHp { get; set; }

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
        var self = m_ObjectHolder.MoveObject;
        ObjectPoolManager.GetInstance().SetGameObject(m_Name, self);
    }
}
