// ノーマルプレイヤークラス

using UnityEngine;

public class NormalPlayer : BasePlayer
{
    void Start()
    {
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
        m_bulletReloadSpeed = 1.0f;
        m_bulletCnt = 0.0f;
    }

    void Update()
    {
        Move();
        Fire();
        PowerUp();
        VersionUp();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.S) && m_bulletCnt >= m_bulletReloadSpeed)
        {
            m_bulletCnt = 0.0f;
            GameObject obj = Instantiate(m_bullet, transform.position, Quaternion.identity);
            obj.GetComponent<NormalBullet>().Create(
                m_bulletAttackPower, m_bulletThroughPower, m_bulletMoveSpeed, m_bulletSize);
        }
        m_bulletCnt += Time.deltaTime;
    }
}
