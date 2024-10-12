using System;
using UniRx;
using UnityEngine;

public class CharaShoot : MonoBehaviour
{
    /// <summary>
    /// スピード
    /// </summary>
    private static readonly float ms_Speed = 10f;

    /// <summary>
    /// 弾を撃ち出す高さ
    /// </summary>
    private static readonly float ms_Height = 0.3f;

    private ObjectHolder ObjectHolder { get; set; }

    /// <summary>
    /// 弾丸プレハブ
    /// </summary>
    [SerializeField]
    private Bullet m_BulletPrefab;

    private void Awake()
    {
        ObjectHolder = GetComponent<ObjectHolder>(); // 

        // 入力購読
        InputManager.GetInstance().InputStartEvent.SubscribeWithState(this, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);
    }

    /// <summary>
    /// 購読用
    /// </summary>
    /// <param name="flag"></param>
    private void DetectInput(KeyCodeFlag flag)
    {
        // 移動検知
        if (DetectInputShoot(flag) == true)
            return;
    }

    /// <summary>
    /// 移動入力検知
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    private bool DetectInputShoot(KeyCodeFlag flag)
    {
        if (flag.HasBitFlag(KeyCodeFlag.Mouse0))
        {
            Shoot();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="dir"></param>
    private void Shoot()
    {
        var rotation = ObjectHolder.CharaObject.transform.rotation;
        Vector3 initPos = new Vector3(transform.position.x, ms_Height, transform.position.z);

        var bulletObject = Instantiate(m_BulletPrefab, initPos, rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.Setup(ms_Speed, 1, 1f);
    }
}