using UnityEngine;
using System.Threading.Tasks;
using Zenject;

/// <summary>
/// 入力機能
/// </summary>
public class PlayerInput : ComponentBase
{
    private CharaAnimator CharaAnimator { get; set; }
    private CharaMove CharaMove { get; set; }
    private CharaShoot CharaShoot { get; set; }

    protected override void Register(ComponentCollector owner)
    {
        base.Register(owner);
        owner.Register(this);
    }

    public override void Initialize()
    {
        base.Initialize();
        CharaAnimator = Owner.GetInterface<CharaAnimator>();
        CharaMove = Owner.GetInterface<CharaMove>();
        CharaShoot = Owner.GetInterface<CharaShoot>();
    }

    /// <summary>
    /// 入力検知 射撃、移動
    /// </summary>
    /// <param name="flag"></param>
    public bool DetectInput(KeyCodeFlag flag)
    {
        // 行動許可ないなら何もしない。
        if (CharaAnimator.IsActing == true)
            return false;

        // 射撃
        if (DetectInputShoot(flag) == true)
            return true;

        // 移動
        if (DetectInputMove(flag) == true)
            return true;
        else
            CharaMove.StopMove();

        return false;
    }

    /// <summary>
    /// 射撃入力検知
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    private bool DetectInputShoot(KeyCodeFlag flag)
    {
        if (flag.HasBitFlag(KeyCodeFlag.Mouse0))
        {
            CharaShoot.Shoot();
            return true;
        }
        return false;
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
        CharaMove.Move(direction);
        return true;
    }
}