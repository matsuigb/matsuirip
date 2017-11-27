// ベースボスクラス

using UnityEngine;

public enum Behavior
{
    None = 0,
    Attack = 1,
    Move = 2,
}

public class BaseBoss : MonoBehaviour
{

    [SerializeField]
    protected GameObject m_bullet;      // 発射する弾のオブジェクト

    // 基本ステータス
    protected float m_hp;             // 体力
    protected float m_recovery;    // 回復力

    // 移動系ステータス
    protected float m_addMoveSpeed;     // 加速度
    protected float m_moveSpeed;        // 移動速度
    private const float MAX_MOVE_SPEED = 10.0f;      // 移動速度上限
    private const float MAX_X_POS = 11.0f;           // 最大左右距離

    void Start()
    {
        // 基本ステータス
        m_hp = 100.0f;
        m_recovery = 2.0f;
        // 移動系ステータス
        m_addMoveSpeed = 0.25f;
        m_moveSpeed = 0.0f;
    }

    void Update()
    {
    }
}
