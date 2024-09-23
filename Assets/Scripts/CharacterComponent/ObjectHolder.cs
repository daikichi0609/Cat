using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MoveObject;
    public GameObject MoveObject => m_MoveObject;

    [SerializeField]
    private GameObject m_CharaObject;
    public GameObject CharaObject => m_CharaObject;
}
