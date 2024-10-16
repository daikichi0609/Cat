using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�����ʒu
    /// </summary>
    private static readonly Vector3 ms_InitPos = new Vector3(0f, 0.5f, 0f);

    private async void Awake()
    {
        CharaObjectManager.GetInstance().CreatePlayer(ms_InitPos);
        CharaObjectManager.GetInstance().CreateEnemy(new Vector3(0f, 0.5f, 3f));

        await FadeManager.GetInstance().TurnBright(); // ���]
    }
}
