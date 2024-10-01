using System.Collections;
using System.Collections.Generic;
using TsugamerLibrary;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField]
    private GameObject[] m_CharaObjectCollector;

    private ObjectPool m_ObjectPool = new ObjectPool();
    
    public GameObject GetGameObject(CHARA_NAME key)
    {
        if(m_ObjectPool.TryGetPoolObject(key.ToString(), out var o) == true)
            return o;
        return Instantiate(m_CharaObjectCollector[(int)key]);
    }

    public void SetGameObject(CHARA_NAME key, GameObject o) => m_ObjectPool.SetObject(key.ToString(), o);
}

public enum CHARA_NAME
{
    BOXMAN = 0,
    ENEMY = 1,
}