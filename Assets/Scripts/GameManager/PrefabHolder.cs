using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TsugamerLibrary;

public class PrefabHolder : Singleton<PrefabHolder>
{
    [SerializeField]
    private GameObject m_BulletPrefab;
    public GameObject BulletPrefab => m_BulletPrefab;

    [SerializeField]
    private GameObject m_BulletBombPrefab;
    public GameObject BulletBombPrefab => m_BulletBombPrefab;
}
