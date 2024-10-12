using System.Collections;
using System.Collections.Generic;
using TsugamerLibrary;
using UnityEngine;
using UniRx;
using System;

public class CharaObjectManager : Singleton<CharaObjectManager>
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private GameObject m_Player;
    public GameObject Player => m_Player;

    /// <summary>
    /// 敵
    /// </summary>
    private List<GameObject> m_EnemyList = new List<GameObject>();

    public IDisposable RegistPlayer(GameObject player)
    {
        if (m_Player != null)
            Debug.Log("プレイヤーの上書きが発生しました。");
        m_Player = player;
        return Disposable.CreateWithState(m_Player, self => self = null);
    }

    public IDisposable RegistEnemy(GameObject enemy)
    {
        if (m_EnemyList != null)
            Debug.Log("プレイヤーの上書きが発生しました。");
        m_EnemyList.Add(enemy);
        return Disposable.CreateWithState((m_EnemyList, enemy), tuple => tuple.Item1.Remove(tuple.Item2));
    }
}
