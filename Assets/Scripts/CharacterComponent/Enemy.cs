using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    private void Start()
    {
        CharaMove = GetComponent<CharaMove>();
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

        if(dir.magnitude > TargetDistance)
            CharaMove.Move(dir.normalized);
    }
}
