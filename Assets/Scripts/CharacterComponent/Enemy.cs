using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ë¨Ç≥
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// ê⁄ãﬂãóó£
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
