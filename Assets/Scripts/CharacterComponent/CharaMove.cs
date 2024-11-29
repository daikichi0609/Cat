using NaughtyAttributes;
using System;
using UnityEngine;

public class CharaMove : ComponentBase
{
    private CharaObjectHolder ObjectHolder { get; set; }
    protected CharaAnimator CharaAnimator { get; set; }

    /// <summary>
    /// 速さ
    /// </summary>
    [ShowNativeProperty]
    public float Speed { get; set; } = 3.0f;

    private IDisposable m_IsMoving;

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        ObjectHolder = Owner.GetInterface<CharaObjectHolder>();
        CharaAnimator = Owner.GetInterface<CharaAnimator>();
    }

    public override void Dispose()
    {
        StopMove();
        base.Dispose();
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="dir"></param>
    private void Move(DIRECTION dir) => Move(dir.ToV3Int());
    public void Move(Vector3 dir)
    {
        Face(dir); // 向き
        MoveInternal(dir); // 移動
        if (m_IsMoving == null)　// アニメーション開始
            m_IsMoving = CharaAnimator.PlayAnimation(ANIMATION_TYPE.MOVE);
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="dir"></param>
    private void MoveInternal(Vector3 dir) => ObjectHolder.MoveObject.transform.position += dir * Speed * Time.deltaTime; // 移動

    /// <summary>
    /// 方向転換
    /// </summary>
    /// <param name="dir"></param>
    public void Face(Vector3 dir) => ObjectHolder.CharaObject.transform.rotation = Quaternion.LookRotation(dir);

    /// <summary>
    /// 移動終了
    /// </summary>
    public void StopMove()
    {
        if (m_IsMoving != null)
        {
            m_IsMoving.Dispose(); // 移動アニメーション終了
            m_IsMoving = null;
        }
    }

    /*
    /// <summary>
    /// カメラ回転
    /// </summary>
    /// <param name="dir"></param>
    private void Rotate(DIRECTION dir)
    {
        var v = dir switch
        {
            DIRECTION.LEFT => new Vector3(0f, -1f, 0f),
            DIRECTION.LOWER_LEFT => new Vector3(0f, -0.5f, 0f),
            DIRECTION.UPPER_LEFT => new Vector3(0f, -0.5f, 0f),

            DIRECTION.RIGHT => new Vector3(0f, 1f, 0f),
            DIRECTION.LOWER_RIGHT => new Vector3(0f, 0.5f, 0f),
            DIRECTION.UPPER_RIGHT => new Vector3(0f, 0.5f, 0f),

            _ => new Vector3(0f, 0f, 0f)
        };

        m_ObjectHolder.MoveObject.transform.eulerAngles += v * ms_CameraSpeed * Time.deltaTime;
    }

    private Vector3 ToCharaMoveDir(DIRECTION dir)
    {
        var r = m_ObjectHolder.MoveObject.transform.eulerAngles; // 現在の向き
        return Quaternion.Euler(r) * dir.ToV3Int(); // 向きに合わせて移動方向を回転
    }
    */
}