// ツーウェイプレイヤークラス

using UnityEngine;

public class TwoWayPlayer : BasePlayer
{
    // ツイン発射場所の補正ベクトル
    Vector3 m_firePos;

    void Start()
    {
        m_firePos = new Vector3(1.0f, 0.0f, 0.0f);

        // プレイヤー基本ステータス
        m_level = 1.0f;
        m_hp = 10.0f;
        m_recovery = 1.0f;
        m_size = 1.0f;
        // プレイヤー移動系ステータス
        m_addMoveSpeed = 0.25f;
        m_moveSpeed = 0.0f;
        // 弾関係ステータス
        m_bulletAttackPower = 1.0f;
        m_bulletThroughPower = 1.0f;
        m_bulletMoveSpeed = 5.0f;
        m_bulletSize = 1.0f;
        // 弾発射系ステータス
        m_bulletReloadSpeed = 0.5f;
        m_bulletCnt = 0.0f;
    }

    void Update()
    {
        Move();
        Fire();
        PowerUp();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.S) && m_bulletCnt >= m_bulletReloadSpeed)
        {
            m_bulletCnt = 0.0f;
            GameObject obj1 = Instantiate(m_bullet, transform.position - m_firePos, Quaternion.identity);
            obj1.GetComponent<NormalBullet>().Create(
                m_bulletAttackPower, m_bulletThroughPower, m_bulletMoveSpeed, m_bulletSize);
            GameObject obj2 = Instantiate(m_bullet, transform.position + m_firePos, Quaternion.identity);
            obj2.GetComponent<NormalBullet>().Create(
                m_bulletAttackPower, m_bulletThroughPower, m_bulletMoveSpeed, m_bulletSize);
        }
        m_bulletCnt += Time.deltaTime;
    }
}
