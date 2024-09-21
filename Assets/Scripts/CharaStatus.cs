using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStatus : MonoBehaviour
{
    [SerializeField]
    private int m_Hp;
    private int m_MaxHp;

    public void Setup(int max)
    {
        m_MaxHp = max;
        m_Hp = m_MaxHp;
    }

    /// <summary>
    /// ダメージ
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>死亡判定</returns>
    public bool Damage(int damage)
    {
        m_Hp = Math.Clamp(m_Hp - damage, 0, m_MaxHp);
        return m_Hp <= 0;
    }
}
