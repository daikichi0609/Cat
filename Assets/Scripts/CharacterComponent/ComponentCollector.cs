using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// コンポーネント集約クラス
/// </summary>
[Serializable]
public class ComponentCollector : MonoBehaviour
{
    private HashSet<ComponentBase> m_Components = new HashSet<ComponentBase>();

    /// <summary>
    /// Disposeするもの
    /// </summary>
    protected CompositeDisposable m_CompositeDisposable = new CompositeDisposable();
    public CompositeDisposable CompositeDisposable => m_CompositeDisposable;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        foreach (var comp in m_Components)
            comp.Initialize();
    }

    /// <summary>
    /// コンポーネント登録
    /// </summary>
    /// <typeparam name="TComp"></typeparam>
    /// <param name="comp"></param>
    public void Register<TComp>(TComp comp)
    {
        if (comp is ComponentBase)
            m_Components.Add(comp as ComponentBase);
    }

    /// <summary>
    /// コンポーネント取得
    /// </summary>
    /// <typeparam name="TComp"></typeparam>
    /// <returns></returns>
    public TComp GetInterface<TComp>() where TComp : ComponentBase
    {
        foreach (var val in m_Components)
            if (val is TComp)
                return val as TComp;

        Debug.LogError("コンポーネントの取得に失敗しました");
        return null;
    }

    /// <summary>
    /// コンポーネント要求
    /// </summary>
    /// <typeparam name="TComp"></typeparam>
    /// <param name="comp"></param>
    /// <returns></returns>
    public bool RequireInterface<TComp>(out TComp comp) where TComp : ComponentBase
    {
        foreach (var val in m_Components)
            if (val is TComp)
            {
                comp = val as TComp;
                return true;
            }

        comp = null;
        return false;
    }

    /// <summary>
    /// 破棄
    /// </summary>
    public void Dispose()
    {
        m_CompositeDisposable.Clear();

        foreach (var comp in m_Components)
            comp.Dispose();
    }
}


public class ComponentBase : MonoBehaviour
{
    /// <summary>
    /// コレクター
    /// </summary>
    protected ComponentCollector Owner { get; set; }

    /// <summary>
    /// Owner取得
    /// </summary>
    private void Awake()
    {
        Owner = GetComponent<ComponentCollector>();
        Register(Owner);
    }

    protected virtual void Register(ComponentCollector owner)
    {
        // コンポーネント登録
    }

    public virtual void Initialize()
    {
        // コンポーネント初期化
    }

    public virtual void Dispose()
    {

    }
}
