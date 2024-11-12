using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAiBase : MonoBehaviour
{
    /// <summary>
    /// 弾丸プレハブ
    /// </summary>
    [SerializeField]
    private GameObject m_BulletPrefab;

    /// <summary>
    /// 射撃
    /// </summary>
    protected virtual void Shoot()
    {
        Instantiate(m_BulletPrefab);
    }
}