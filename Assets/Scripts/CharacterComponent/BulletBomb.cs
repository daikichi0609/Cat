using UnityEngine;
using NaughtyAttributes;
using System.Threading.Tasks;
using System.Threading;

public class BulletBomb : ComponentBase
{
    [ShowNativeProperty]
    private float CurrentLimit { get; set; }

    [ShowNativeProperty]
    private int Damage { get; set; }
    [ShowNativeProperty]
    private CHARA_TYPE TargetType { get; set; }

    private Task ShootTask { get; set; }
    private CancellationTokenSource Cts { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Dispose()
    {
        if (ShootTask?.IsCompleted == false)
            Cts.Cancel();

        base.Dispose();
        Destroy(gameObject);
    }

    public void Shoot(int damage, CHARA_TYPE target, Vector3 targetPos)
    {
        Damage = damage;
        TargetType = target;

        Cts = new CancellationTokenSource();
        ShootTask = ShootInternal(gameObject, 1f, gameObject.transform.position, targetPos, 1f);
    }

    public async Task ShootInternal(GameObject self, float height, Vector3 start, Vector3 end, float duration)
    {
        // 中点を求める
        Vector3 half = end - start * 0.50f + start;
        half.y += Vector3.up.y + height;
        await LerpShoot(self, start, half, end, duration);
    }

    private async Task LerpShoot(GameObject self, Vector3 start, Vector3 half, Vector3 end, float time, float rate = 0f)
    {
        float startTime = Time.timeSinceLevelLoad;
        while (rate < 1.0f)
        {
            float diff = Time.timeSinceLevelLoad - startTime;
            rate = diff / time;
            self.transform.position = CalcLerpPoint(start, half, end, rate);
            await Task.Delay(1);
        }
        Explosion();

        Vector3 CalcLerpPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            var a = Vector3.Lerp(p0, p1, t);
            var b = Vector3.Lerp(p1, p2, t);
            return Vector3.Lerp(a, b, t);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        var target = col.gameObject;
        if (target.TryGetComponent<CharaStatus>(out var status) == false || status.Type.HasBitFlag(TargetType) == false)
            return;

        status.Damage(Damage);
        Explosion();
    }

    private void Explosion()
    {

        Dispose();
    }
}
