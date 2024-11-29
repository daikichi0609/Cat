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
    private ComponentCollector m_Player;
    public ComponentCollector Player => m_Player;

    /// <summary>
    /// 敵
    /// </summary>
    private List<ComponentCollector> m_EnemyList = new List<ComponentCollector>();

    /// <summary>
    /// プレイヤー作成
    /// </summary>
    /// <returns></returns>
    public void CreatePlayer(Vector3 pos)
    {
        if (ObjectPoolManager.GetInstance().TryGetGameObject(CHARA_NAME.BOXMAN, out var player) == false)
            player = Instantiate(m_PlayerPrefab, pos, Quaternion.identity);

        var collector = player.GetComponent<ComponentCollector>();
        var d = RegistPlayer(collector);
        collector.CompositeDisposable.Add(d);
    }

    private IDisposable RegistPlayer(ComponentCollector player)
    {
        if (m_Player != null)
            Debug.Log("プレイヤーの上書きが発生しました。");
        m_Player = player;

        SetupPlayer(m_Player);
        return Disposable.CreateWithState(m_Player, self =>
        {
            self.Dispose();
            var moveObject = self.GetInterface<CharaObjectHolder>().MoveObject;
            moveObject.SetActive(false);
            ObjectPoolManager.GetInstance().SetGameObject(CHARA_NAME.BOXMAN, moveObject);
            self = null;
        });

        /// <summary>
        /// プレイヤーセットアップ
        /// </summary>
        /// <param name="player"></param>
        void SetupPlayer(ComponentCollector player)
        {
            player.Initialize();

            // カメラ登録
            var objectHolder = player.GetInterface<CharaObjectHolder>();
            CameraHandler.GetInstance().SetParent(objectHolder.MoveObject);

            // プレイヤー入力受付
            var pInput = player.GetInterface<PlayerInput>();
            InputManager.GetInstance().InputEvent.SubscribeWithState(pInput, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(player.CompositeDisposable);
        }
    }

    /// <summary>
    /// エネミー作成
    /// </summary>
    /// <returns></returns>
    public void CreateEnemy(Vector3 pos)
    {
        if (ObjectPoolManager.GetInstance().TryGetGameObject(CHARA_NAME.ENEMY, out var enemy) == false)
            enemy = Instantiate(m_EnemyPrefab, pos, Quaternion.identity);

        var collector = enemy.GetComponent<ComponentCollector>();
        var moveObject = collector.GetInterface<CharaObjectHolder>().MoveObject;

        // ----- Hpバーセット ----- //
        var status = collector.GetInterface<CharaStatus>();
        if (status.HpBar == null)
        {
            var bar = Instantiate(m_HpBarPrefab);
            var canvas = Instantiate(m_WorldCanvasPrefab);
            bar.transform.SetParent(canvas.transform);
            canvas.transform.position = moveObject.transform.position + ms_HpBarOffset;
            canvas.transform.SetParent(moveObject.transform);
            status.HpBar = bar.GetComponent<Slider>();
        }
        // ----- //

        var d = RegistEnemy(collector);
        collector.CompositeDisposable.Add(d);
    }

    private IDisposable RegistEnemy(ComponentCollector enemy)
    {
        m_EnemyList.Add(enemy);
        enemy.Initialize();

        return Disposable.CreateWithState((enemy, m_EnemyList), tuple =>
        {
            var enemy = tuple.Item1;
            enemy.Dispose();
            var moveObject = enemy.GetInterface<CharaObjectHolder>().MoveObject;
            moveObject.SetActive(false);
            tuple.Item2.Remove(tuple.Item1);
            ObjectPoolManager.GetInstance().SetGameObject(CHARA_NAME.ENEMY, moveObject);
        });
    }
}
