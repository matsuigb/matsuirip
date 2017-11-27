// ベースバレットクラス

using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private const float DEATH_POS_X = 15.0f;      // 移動上限座標(X)
    private const float DEATH_POS_Y = 15.0f;      // 移動上限座標(Y)

    // 弾基本ステータス
    protected float m_attackPower;          // 攻撃力
    protected float m_throughPower;         // 貫通力
    protected float m_moveSpeed;            // 速度
    protected float m_size;                 // 大きさ

    // Instantiateされた時に呼ばれる
    public void Create(float _atkPower, float _throughPower, float _moveSpeed, float _size)
    {
        m_attackPower = _atkPower;
        m_throughPower = _throughPower;
        m_moveSpeed = _moveSpeed;
        m_size = _size;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    protected void CheckDeath()
    {
        if (transform.position.y > DEATH_POS_X)
        {
            Destroy(gameObject, 0.0f);
        }
        if (transform.position.x < -DEATH_POS_X || transform.position.x > DEATH_POS_X)
        {
            Destroy(gameObject, 0.0f);
        }
    }
}
