using System.Collections;
using System.Collections.Generic;
using TsugamerLibrary;
using UnityEngine;
using UniRx;
using System;
using Zenject;
using UnityEngine.UI;

public class CharaObjectManager : Singleton<CharaObjectManager>
{
    [SerializeField]
    private GameObject m_PlayerPrefab;

    [SerializeField]
    private GameObject m_EnemyPrefab;

    [SerializeField]
    private GameObject m_HpBarPrefab;

    [SerializeField]
    private GameObject m_WorldCanvasPrefab;

    private static readonly Vector3 ms_HpBarOffset = new Vector3(0f, 1f, 0f);

    /// <summary>
    /// プレイヤー
    /// </summary>
    private GameObject m_Player;
    public GameObject Player => m_Player;

    /// <summary>
    /// 敵
    /// </summary>
    private List<GameObject> m_EnemyList = new List<GameObject>();

    /// <summary>
    /// プレイヤー作成
    /// </summary>
    /// <returns></returns>
    public IDisposable CreatePlayer(Vector3 pos)
    {
        if (ObjectPoolManager.GetInstance().TryGetGameObject(CHARA_NAME.BOXMAN, out var player) == false)
            player = Instantiate(m_PlayerPrefab, pos, Quaternion.identity);

        return RegistPlayer(player);
    }

    private IDisposable RegistPlayer(GameObject player)
    {
        if (m_Player != null)
            Debug.Log("プレイヤーの上書きが発生しました。");
        m_Player = player;

        SetupPlayer(m_Player);
        return Disposable.CreateWithState(m_Player, self =>
        {
            self.SetActive(false);
            ObjectPoolManager.GetInstance().SetGameObject(CHARA_NAME.BOXMAN, self);
            self = null;
        });

        /// <summary>
        /// プレイヤーセットアップ
        /// </summary>
        /// <param name="player"></param>
        void SetupPlayer(GameObject player)
        {
            // カメラ登録
            var objectHolder = player.GetComponent<ObjectHolder>();
            CameraHandler.GetInstance().SetParent(objectHolder.MoveObject);

            // 移動入力受付
            var move = player.GetComponent<CharaMove>();
            InputManager.GetInstance().InputEvent.SubscribeWithState(move, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);

            // 射撃入力購読
            var shoot = player.GetComponent<CharaShoot>();
            InputManager.GetInstance().InputStartEvent.SubscribeWithState(shoot, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);
        }
    }

    /// <summary>
    /// エネミー作成
    /// </summary>
    /// <returns></returns>
    public IDisposable CreateEnemy(Vector3 pos)
    {
        if (ObjectPoolManager.GetInstance().TryGetGameObject(CHARA_NAME.ENEMY, out var enemy) == false)
            enemy = Instantiate(m_EnemyPrefab, pos, Quaternion.identity);

        // ----- Hpバーセット ----- //
        var status = enemy.GetComponent<CharaStatus>();
        var bar = Instantiate(m_HpBarPrefab);
        var canvas = Instantiate(m_WorldCanvasPrefab);
        bar.transform.SetParent(canvas.transform);
        canvas.transform.position = enemy.transform.position + ms_HpBarOffset;
        canvas.transform.SetParent(enemy.transform);
        status.HpBar = bar.GetComponent<Slider>();
        // ----- //

        return RegistEnemy(enemy);
    }

    private IDisposable RegistEnemy(GameObject enemy)
    {
        m_EnemyList.Add(enemy);
        return Disposable.CreateWithState((enemy, m_EnemyList), tuple =>
        {
            tuple.Item1.SetActive(false);
            tuple.Item2.Remove(tuple.Item1);
            ObjectPoolManager.GetInstance().SetGameObject(CHARA_NAME.ENEMY, tuple.Item1);
        });
    }
}
