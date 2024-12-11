using UnityEngine;
using NaughtyAttributes;
using System.Threading.Tasks;
using System.Threading;

public class Bullet : ComponentBase
{
    [ShowNativeProperty]
    private int Damage { get; set; }
    [ShowNativeProperty]
    private float TimeLimit { get; set; }
    [ShowNativeProperty]
    private CHARA_TYPE TargetType { get; set; }

    private Task ShootTask { get; set; }
    private CancellationTokenSource Cts { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Dispose()
    {
        if (ShootTask?.IsCompleted == false)
            Cts.Cancel();

        base.Dispose();
        Destroy(gameObject);
    }

    public void Shoot(float speed, int damage, float limit, CHARA_TYPE target)
    {
        Damage = damage;
        TimeLimit = limit;
        TargetType = target;

        Cts = new CancellationTokenSource();
        ShootTask = ShootInternal(speed, Cts);
    }

    private async Task ShootInternal(float speed, CancellationTokenSource cts)
    {
        float currentLimit = 0f;
        while (currentLimit < TimeLimit && Cts.IsCancellationRequested == false)
        {
            currentLimit += Time.deltaTime;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            await Task.Delay(1);
        }
        Dispose();
    }

    private void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject;
        if (target.TryGetComponent<CharaStatus>(out var status) == false || status.Type.HasBitFlag(TargetType) == false)
            return;

        status.Damage(Damage);
#if DEBUG
        Debug.Log("ヒット");
#endif 
        Dispose();
    }
}
