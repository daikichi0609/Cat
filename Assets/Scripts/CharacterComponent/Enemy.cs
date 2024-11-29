using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ComponentBase
{
    /// <summary>
    /// 速さ
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// 接近距離
    /// </summary>
    public float TargetDistance { get; set; }

    private CharaMove CharaMove { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        CharaMove = Owner.GetInterface<CharaMove>();
        CharaMove.Speed = 1.0f;
    }

    private void Update()
    {
        Chase();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Chase()
    {
        var target = CharaObjectManager.GetInstance().Player;
        var targetPos = target.transform.position;
        var dir = targetPos - this.transform.position;

        if (dir.magnitude > TargetDistance)
            CharaMove.Move(dir.normalized);
    }
}
