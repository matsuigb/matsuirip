using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPlayer : BasePlayer
{

    [SerializeField]
    private GameObject m_sniperFirePos;

    [SerializeField]
    private GameObject m_hardSniperPlayer;

    private const int ADD_BULLET_HP = 3;
    private const int ADD_BULLET_SPEED = 5;
    private const int ADD_BULLET_DAMAGE = 2;

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
        Evolution();
    }

    protected override void Attack()
    {
        m_atkCnt += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_atkCnt >= m_reloadTime)
        {
            m_fireSE.PlayOneShot(m_fireSE.clip);

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_sniperFirePos.transform.position, Quaternion.identity, m_playerBulletParent.transform);
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

    // ----------------------------------------------------------------- //
    ////////////////////////////// 進化系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected override void Evolution()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // 進化していなくて進化可能であれば
            if (m_level >= LAST_EVOL_NEED_LEVEL)
            {
                Instantiate(m_hardSniperPlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
