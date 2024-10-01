using UnityEngine;
using NaughtyAttributes;

public class Bullet : MonoBehaviour
{
    [ShowNativeProperty]
    private float CurrentLimit { get; set; }

    [ShowNativeProperty]
    private float Speed { get; set; }
    [ShowNativeProperty]
    private int Damage { get; set; }
    [ShowNativeProperty]
    private float TimeLimit { get; set; }

    public void Setup(float speed, int damage, float limit)
    {
        Speed = speed;
        Damage = damage;
        TimeLimit = limit;
    }

    private void Update()
    {
        CurrentLimit += Time.deltaTime;
        if (CurrentLimit >= TimeLimit)
        {
            Destroy(gameObject);
            return;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    private void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject;
        if (target.TryGetComponent<CharaStatus>(out var status) == false)
            return;

        status.Damage(Damage);
        Destroy(gameObject);
    }
}
