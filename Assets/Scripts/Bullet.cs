using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private int m_Damage;
    [SerializeField]
    private int m_TimeLimit;

    [SerializeField]
    private float m_CurrentLimit;

    public void Setup(float speed, int damage, int limit)
    {
        m_Speed = speed;
        m_Damage = damage;
        m_TimeLimit = limit;
    }

    private void Update()
    {
        m_CurrentLimit += Time.deltaTime;
        if (m_CurrentLimit >= m_TimeLimit)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += new Vector3(0f, 0f, m_Speed);
    }

    private void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject;
        if (target.TryGetComponent<CharaStatus>(out var status) == false)
            return;

        if (status.Damage(m_Damage) == false)
            return;

        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}
