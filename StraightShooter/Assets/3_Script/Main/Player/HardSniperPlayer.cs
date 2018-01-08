using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardSniperPlayer : BasePlayer {

    [SerializeField]
    private GameObject m_hardSniperFirePos;

    private const int ADD_BULLET_HP = 4;
    private const int ADD_BULLET_SPEED = 5;
    private const int ADD_BULLET_DAMAGE = 4;

    void Start()
    {
        m_playerBulletParent = GameObject.Find("PlayerBulletParent");
        m_playerInfo = GameObject.FindWithTag("PlayerInfo");
        m_fireSE = GetComponent<AudioSource>();
    }

    void Update()
    {
        PowerUp();
        Move();
        Attack();
        HEAL();
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 攻撃系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected override void Attack()
    {
        m_atkCnt += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_atkCnt >= (m_reloadTime / 6.0f))
        {
            m_fireSE.PlayOneShot(m_fireSE.clip);

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_hardSniperFirePos.transform.position, Quaternion.identity, m_playerBulletParent.transform);
            PlayerBullet playerBulletComp = playerBulletObj.GetComponent<PlayerBullet>();

            if (playerBulletComp != null)
            {
                playerBulletComp.Init(directionVec,
                    m_bulletHPLevel + ADD_BULLET_HP,
                    m_bulletSpeedLevel + ADD_BULLET_SPEED,
                    m_bulletDamageLevel + ADD_BULLET_DAMAGE);
            }
            else
            {
                Debug.Log("playerBulletCompにてnullが発生しています");
            }
        }
    }
}
