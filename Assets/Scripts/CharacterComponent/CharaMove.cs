using System;
using UniRx;
using UnityEngine;

public class CharaMove : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    private static readonly float ms_MoveSpeed = 3f;

    /// <summary>
    /// カメラ旋回スピード
    /// </summary>
    private static readonly float ms_CameraSpeed = 40f;

    private ObjectHolder ObjectHolder { get; set; }

    [SerializeField]
    private Animator m_CharaAnimator;

    private IDisposable m_IsMoving;

    private void Awake()
    {
        ObjectHolder = GetComponent<ObjectHolder>();

        // 入力購読
        InputManager.GetInstance().InputEvent.SubscribeWithState(this, (input, self) => self.DetectInput(input.KeyCodeFlag)).AddTo(this);

        // カメラ登録
        CameraHandler.GetInstance().SetParent(ObjectHolder.MoveObject);
    }

    /// <summary>
    /// 購読用
    /// </summary>
    /// <param name="flag"></oparam>
    private void DetectInput(KeyCodeFlag flag)
    {
        // 移動検知
        if (DetectInputMove(flag) == true)
            return;
        else
        {
            m_IsMoving?.Dispose(); // 移動アニメーション終了
            m_IsMoving = null;
        }
    }

    /// <summary>
    /// 移動入力検知
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    private bool DetectInputMove(KeyCodeFlag flag)
    {
        var direction = new Vector3Int();

        if (flag.HasBitFlag(KeyCodeFlag.W))
            direction += new Vector3Int(0, 0, 1);

        if (flag.HasBitFlag(KeyCodeFlag.A))
            direction += new Vector3Int(-1, 0, 0);

        if (flag.HasBitFlag(KeyCodeFlag.S))
            direction += new Vector3Int(0, 0, -1);

        if (flag.HasBitFlag(KeyCodeFlag.D))
            direction += new Vector3Int(1, 0, 0);

        // 入力なし
        if (direction == new Vector3Int(0, 0, 0))
            return false;

        // 移動
        Move(direction.ToDirEnum());
        return true;
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
            m_IsMoving = PlayAnimation(ANIMATION_TYPE.MOVE);
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="dir"></param>
    private void MoveInternal(Vector3 dir) => ObjectHolder.MoveObject.transform.position += dir * ms_MoveSpeed * Time.deltaTime; // 移動

    /// <summary>
    /// 方向転換
    /// </summary>
    /// <param name="dir"></param>
    private void Face(Vector3 dir) => ObjectHolder.CharaObject.transform.rotation = Quaternion.LookRotation(dir);

    /// <summary>
    /// アニメーション
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IDisposable PlayAnimation(ANIMATION_TYPE type)
    {
        var key = GetKey(type);
        m_CharaAnimator.SetBool(key, true);
        return Disposable.CreateWithState((this, key), tuple => tuple.Item1.m_CharaAnimator.SetBool(tuple.key, false));
    }

    /// <summary>
    /// アニメーション切り替えキー取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetKey(ANIMATION_TYPE type)
    {
        string key = type switch
        {
            ANIMATION_TYPE.IDLE => "",
            ANIMATION_TYPE.MOVE => "IsRunning",
            ANIMATION_TYPE.ATTACK => "IsAttacking",
            _ => "",
        };

        return key;
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


/// <summary>
/// アニメーションパターン定義
/// </summary>
public enum ANIMATION_TYPE
{
    /// <summary>
    /// 通常
    /// </summary>
    IDLE,

    /// <summary>
    /// 移動
    /// </summary>
    MOVE,

    /// <summary>
    /// 攻撃
    /// </summary>
    ATTACK,
}


