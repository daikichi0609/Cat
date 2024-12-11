using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤー初期位置
    /// </summary>
    private static readonly Vector3 ms_InitPos = new Vector3(0f, 0.05f, 0f);

    private async void Start()
    {
        CharaObjectManager.GetInstance().CreatePlayer(ms_InitPos); // プレイヤー生成
        await FadeManager.GetInstance().TurnBright(); // 明転
    }
}
