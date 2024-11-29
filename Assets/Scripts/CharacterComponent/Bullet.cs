using UnityEngine;
using NaughtyAttributes;

public class Bullet : ComponentBase
{
    [ShowNativeProperty]
    private float CurrentLimit { get; set; }

    [ShowNativeProperty]
    private float Speed { get; set; }
    [ShowNativeProperty]
    private int Damage { get; set; }
    [ShowNativeProperty]
    private float TimeLimit { get; set; }
    [ShowNativeProperty]
    private CHARA_TYPE TargetType { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public void Setup(float speed, int damage, float limit, CHARA_TYPE target)
    {
        Speed = speed;
        Damage = damage;
        TimeLimit = limit;
        TargetType = target;
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
        if (target.TryGetComponent<CharaStatus>(out var status) == false || status.Type.HasBitFlag(TargetType) == false)
            return;

        status.Damage(Damage);
        Destroy(gameObject);
    }
}
