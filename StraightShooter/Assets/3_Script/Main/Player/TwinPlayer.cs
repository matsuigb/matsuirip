using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinPlayer : BasePlayer
{

    [SerializeField]
    private GameObject[] m_twinFirePos;

    [SerializeField]
    private GameObject m_triplePlayer;

    private const int ADD_BULLET_HP = 3;
    private const int ADD_BULLET_SPEED = 2;
    private const int ADD_BULLET_DAMAGE = 4;

    private const int FIRE_POS_VAL = 2;
    private int firePosNum = 0;     // 最後どちらの砲門で打ったか
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

    // ----------------------------------------------------------------- //
    ////////////////////////////// 攻撃系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected override void Attack()
    {
        m_atkCnt += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_atkCnt >= (m_reloadTime / 2.0f))
        {
            m_fireSE.PlayOneShot(m_fireSE.clip);

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_twinFirePos[firePosNum].transform.position, Quaternion.identity, m_playerBulletParent.transform);
            PlayerBullet playerBulletComp = playerBulletObj.GetComponent<PlayerBullet>();

            // 砲門を変える
            if (firePosNum == 0)
            {
                firePosNum = 1;
            }
            else
            {
                firePosNum = 0;
            }

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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 進化していなくて進化可能であれば
            if (m_level >= LAST_EVOL_NEED_LEVEL)
            {
                Instantiate(m_triplePlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
