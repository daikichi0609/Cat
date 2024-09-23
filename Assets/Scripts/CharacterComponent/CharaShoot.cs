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

    /// <summary>
    /// 弾丸プレハブ
    /// </summary>
    [SerializeField]
    private Bullet m_BulletPrefab;

    [SerializeField]
    private InputManager m_InputManager;
    [SerializeField]
    private CameraHandler m_CameraHandler;

    private void Awake()
    {
        // 入力購読
        m_InputManager.InputStartEvent.SubscribeWithState(this, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);
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
        var mainCamera = m_CameraHandler.MainCamera.GetComponent<Camera>();
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var raycastHitList = Physics.RaycastAll(ray).AsSpan();

        if (raycastHitList.IsEmpty == false)
        {
            var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList[0].point);
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            Vector3 targetPos = mainCamera.ScreenToWorldPoint(mousePosition);
            targetPos.y = ms_Height;
            Vector3 initPos = new Vector3(transform.position.x, targetPos.y, transform.position.z);

            var bulletObject = Instantiate(m_BulletPrefab, initPos, Quaternion.identity);
            var bullet = bulletObject.GetComponent<Bullet>();
            bullet.Setup(targetPos, ms_Speed, 1, 1f);
        }
    }
}