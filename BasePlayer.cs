// ベースプレイヤークラス

using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_bullet;      // 発射する弾のオブジェクト

    // プレイヤー基本ステータス
    protected float m_level;          // レベル(経験値)
    protected float m_hp;             // 体力
    protected float m_recovery;       // 回復力
    protected float m_size;           // 大きさ

    // プレイヤー移動系ステータス
    protected float m_addMoveSpeed;     // 加速度
    protected float m_moveSpeed;        // 移動速度
    private const float MAX_MOVE_SPEED = 3.0f;      // 移動速度上限
    private const float SUB_SPEED = 0.01f;          // 減速速度
    private const float MAX_X_POS = 7.0f;           // 最大左右距離

    // 弾関係ステータス
    protected float m_bulletAttackPower;          // 玉の攻撃力
    protected float m_bulletThroughPower;         // 玉の貫通力
    protected float m_bulletMoveSpeed;            // 玉の速度
    protected float m_bulletSize;                 // 玉の大きさ
    // 弾発射系ステータス
    protected float m_bulletReloadSpeed;          // 玉のリロード速度
    protected float m_bulletCnt;                  // 最後に弾を撃ってからのカウンタ

    void Start()
    {
    }

    void Update()
    {
    }

    protected void Move()
    {
        // キーで移動
        if (Input.GetKey(KeyCode.A))
        {
            m_moveSpeed -= m_addMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_moveSpeed += m_addMoveSpeed * Time.deltaTime;
        }
        else
        {
            if (m_moveSpeed > 0.0f)
            {
                m_moveSpeed -= SUB_SPEED * Time.deltaTime;
            }
            if (m_moveSpeed < -0.0f)
            {
                m_moveSpeed += SUB_SPEED * Time.deltaTime;
            }
        }

        // 移動速度は一定以上にならないようにする
        if (m_moveSpeed < -MAX_MOVE_SPEED)
        {
            m_moveSpeed = -MAX_MOVE_SPEED;
        }
        if (m_moveSpeed > MAX_MOVE_SPEED)
        {
            m_moveSpeed = MAX_MOVE_SPEED;
        }

        // 移動
        transform.position = new Vector3(
            transform.position.x + m_moveSpeed,transform.position.y, transform.position.z);
        // 移動結果がはみ出てたら修正
        if (transform.position.x > MAX_X_POS)
        {
            transform.position = new Vector3(MAX_X_POS, transform.position.y, transform.position.z);
            m_moveSpeed = 0.0f;
        }
        if (transform.position.x < -MAX_X_POS)
        {
            transform.position = new Vector3(-MAX_X_POS, transform.position.y, transform.position.z);
            m_moveSpeed = 0.0f;
        }
    }

    // パワーアップ todo
    protected void PowerUp() {
        // キーで移動
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_addMoveSpeed += 0.1f;
        }
         if (Input.GetKey(KeyCode.W))
        {
            m_bulletReloadSpeed -= 0.01f;
        }
         if (Input.GetKey(KeyCode.E))
        {
            m_bulletMoveSpeed += 0.01f;
        }
    }

    // 進化 todo
    protected void VersionUp()
    {
        //
    }
}
