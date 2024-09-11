﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UniRx;
using UniRx.Triggers;
using System;
using System.Threading.Tasks;

public interface IPlayerLoopManager
{
    IObservable<Unit> GetInitEvent { get; }
    IObservable<Unit> GetUpdateEvent { get; }
}

public class PlayerLoopManager : MonoBehaviour, IPlayerLoopManager
{
    /// <summary>
    /// 初期化処理メソッドまとめ
    /// </summary>
    private Subject<Unit> m_Initialize = new Subject<Unit>();
    IObservable<Unit> IPlayerLoopManager.GetInitEvent => m_Initialize;

    /// <summary>
    /// 毎F処理メソッドまとめ
    /// </summary>
    private Subject<Unit> m_Update = new Subject<Unit>();
    IObservable<Unit> IPlayerLoopManager.GetUpdateEvent => m_Update;

    private static readonly int FRAME_RATE = 60;

    //初期化処理呼び出し
    private void Start()
    {
        Application.targetFrameRate = FRAME_RATE;
        m_Initialize.OnNext(Unit.Default);
    }

    //Update関数は基本的にここだけ
    private void Update()
    {
        m_Update.OnNext(Unit.Default);
    }
}
