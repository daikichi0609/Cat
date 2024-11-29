using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaObjectHolder : ComponentBase
{
    [SerializeField]
    private GameObject m_MoveObject;
    public GameObject MoveObject => m_MoveObject;

    [SerializeField]
    private GameObject m_CharaObject;
    public GameObject CharaObject => m_CharaObject;

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }
}
