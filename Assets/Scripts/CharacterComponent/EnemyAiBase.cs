using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAiBase : ComponentBase
{
    /// <summary>
    /// 弾丸プレハブ
    /// </summary>
    [SerializeField]
    private GameObject m_BulletPrefab;

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    /// <summary>
    /// 射撃
    /// </summary>
    protected virtual void Shoot()
    {
        Instantiate(m_BulletPrefab);
    }
}