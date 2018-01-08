using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePlayer : BasePlayer {

    [SerializeField]
    private GameObject[] m_tripleFirePos;

    private const int ADD_BULLET_HP = 5;
    private const int ADD_BULLET_SPEED = 6;
    private const int ADD_BULLET_DAMAGE = 2;

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
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 攻撃系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected override void Attack()
    {
        m_atkCnt += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_atkCnt >= (m_reloadTime / 3.0f))
        {
            m_fireSE.PlayOneShot(m_fireSE.clip);

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_tripleFirePos[firePosNum].transform.position, Quaternion.identity, m_playerBulletParent.transform);
            PlayerBullet playerBulletComp = playerBulletObj.GetComponent<PlayerBullet>();

            // 最後の砲門
            GameObject playerBulletObj2 = Instantiate(m_playerBullet, m_tripleFirePos[2].transform.position, Quaternion.identity, m_playerBulletParent.transform);
            PlayerBullet playerBulletComp2 = playerBulletObj2.GetComponent<PlayerBullet>();

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

            if (playerBulletComp2 != null)
            {
                playerBulletComp2.Init(directionVec,
                    m_bulletHPLevel + ADD_BULLET_HP,
                    m_bulletSpeedLevel + ADD_BULLET_SPEED,
                    m_bulletDamageLevel + ADD_BULLET_DAMAGE);
            }
            else
            {
                Debug.Log("playerBulletComp2にてnullが発生しています");
            }
        }
    }
}
