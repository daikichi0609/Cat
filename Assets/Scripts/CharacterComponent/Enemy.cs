using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharaMove CharaMove { get; set; }

    private void Start()
    {
        CharaMove = GetComponent<CharaMove>();
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
        var dir = (this.transform.position - targetPos).normalized;
        CharaMove.Move(dir);
    }
}
