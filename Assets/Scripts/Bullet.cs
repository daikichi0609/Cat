using UnityEngine;
using NaughtyAttributes;

public class Bullet : MonoBehaviour
{
    [ShowNativeProperty]
    private Vector3 TargetPos { get; set; }
    [ShowNativeProperty]
    private float Speed { get; set; }
    [ShowNativeProperty]
    private int Damage { get; set; }
    [ShowNativeProperty]
    private int TimeLimit { get; set; }

    [SerializeField]
    private float m_CurrentLimit;

    public void Setup(Vector3 targetPos, float speed, int damage, int limit)
    {
        TargetPos = targetPos;
        Speed = speed;
        Damage = damage;
        TimeLimit = limit;

        transform.LookAt(TargetPos);
    }

    private void Update()
    {
        m_CurrentLimit += Time.deltaTime;
        if (m_CurrentLimit >= TimeLimit)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += new Vector3(0f, 0f, Speed);
    }

    private void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject;
        if (target.TryGetComponent<CharaStatus>(out var status) == false)
            return;

        if (status.Damage(Damage) == false)
            return;

        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}
