using System;
using System.Threading.Tasks;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class CharaShoot : ComponentBase
{
    /// <summary>
    /// 弾を撃ち出す高さ
    /// </summary>
    private static readonly float ms_Height = 0.25f;

    private static readonly float ms_BulletSpeed = 10f;
    private static readonly int ms_BulletDamage = 2;
    private static readonly float ms_BulletTimeLimit = 1f;
    private static readonly int ms_BurstCount = 3;

    private CharaObjectHolder ObjectHolder { get; set; }
    private CharaMove CharaMove { get; set; }
    private CharaAnimator CharaAnimator { get; set; }

    /// <summary>
    /// 着弾対象
    /// </summary>
    [ShowNativeProperty]
    private CHARA_TYPE TargetType { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        ObjectHolder = Owner.GetInterface<CharaObjectHolder>();
        CharaMove = Owner.GetInterface<CharaMove>();
        CharaAnimator = Owner.GetInterface<CharaAnimator>();

        var status = Owner.GetInterface<CharaStatus>();
        var myType = status.Type;
        if (myType.HasBitFlag(CHARA_TYPE.ENEMY) == true)
            TargetType = CHARA_TYPE.PLAYER | CHARA_TYPE.ALLY;
        else
            TargetType = CHARA_TYPE.ENEMY;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="dir"></param>
    public void ShootBullet()
    {
        var charaPos = ObjectHolder.MoveObject.transform.position; // キャラの座標
        var distance = Vector3.Distance(charaPos, Camera.main.transform.position); // キャラとカメラの距離
        var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // マウスの3D座標
        var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        worldPosition.y = charaPos.y;
        var dir = (worldPosition - charaPos).normalized;
        CharaMove.Face(dir); // 向く

        var t = CharaAnimator.RegisterAct();
        ShootBulletAsync(t, ms_BurstCount);
    }

    private async void ShootBulletAsync(IDisposable t, int count)
    {
        for (int i = 0; i < count; i++)
        {
            ShootInternal();
            await Task.Delay(100);
        }
        t.Dispose();

        void ShootInternal()
        {
            var rotation = ObjectHolder.CharaObject.transform.rotation; // キャラの方向
            Vector3 initPos = new Vector3(transform.position.x, ms_Height, transform.position.z); // 初期生成座標

            var bulletObject = Instantiate(PrefabHolder.GetInstance().BulletPrefab, initPos, rotation); // 弾丸生成
            var bullet = bulletObject.GetComponent<Bullet>();
            bullet.Shoot(ms_BulletSpeed, ms_BulletDamage, ms_BulletTimeLimit, TargetType); // 発射
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="dir"></param>
    public void ShootBulletBomb()
    {
        var charaPos = ObjectHolder.MoveObject.transform.position; // キャラの座標
        var distance = Vector3.Distance(charaPos, Camera.main.transform.position); // キャラとカメラの距離
        var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // マウスの3D座標
        var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
#if DEBUG
        Debug.Log(worldPosition);
#endif 

        worldPosition.y = ms_Height;
        var dir = (worldPosition - charaPos).normalized;
        CharaMove.Face(dir); // 向く

        var t = CharaAnimator.RegisterAct();
        ShootBulletBombAsync(t, worldPosition);
    }

    private async void ShootBulletBombAsync(IDisposable t, Vector3 targetPos)
    {
        ShootInternal(targetPos);
        await Task.Delay(150);
        t.Dispose();

        void ShootInternal(Vector3 targetPos)
        {
            var rotation = ObjectHolder.CharaObject.transform.rotation; // キャラの方向
            Vector3 initPos = new Vector3(transform.position.x, ms_Height, transform.position.z); // 初期生成座標

            var bulletObject = Instantiate(PrefabHolder.GetInstance().BulletBombPrefab, initPos, rotation); // 弾丸生成
            var bullet = bulletObject.GetComponent<BulletBomb>();
            bullet.Shoot(ms_BulletDamage, TargetType, targetPos); // セットパラメタ
        }
    }
}