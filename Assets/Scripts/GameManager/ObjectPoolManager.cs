using System.Collections;
using System.Collections.Generic;
using TsugamerLibrary;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private ObjectPool m_ObjectPool = new ObjectPool();

    public bool TryGetGameObject(CHARA_NAME key, out GameObject o) => m_ObjectPool.TryGetPoolObject(key.ToString(), out o);

    public void SetGameObject(CHARA_NAME key, GameObject o) => m_ObjectPool.SetObject(key.ToString(), o);
}