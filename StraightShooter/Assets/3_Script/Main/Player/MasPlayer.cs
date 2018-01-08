using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasPlayer : BasePlayer
{
    [SerializeField]
    private GameObject m_masFirePos;

    [SerializeField]
    private GameObject m_hardMasPlayer;

    private const int ADD_BULLET_HP = 1;
    private const int ADD_BULLET_SPEED = 1;
    private const int ADD_BULLET_DAMAGE = 1;

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
        if (Input.GetMouseButton(0) && m_atkCnt >= (m_reloadTime / 5.0f))
        {
            if (!m_fireSE.isPlaying)
            {
                m_fireSE.PlayOneShot(m_fireSE.clip);
            }

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            // マシンガンはななめ撃ちだと安定しない
            directionVec.z = 0.0f;
            directionVec.Normalize();
            directionVec.x *= (Random.value * 3.0f + 1.5f);
            directionVec.y *= (Random.value * 3.0f + 1.5f);

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_masFirePos.transform.position, Quaternion.identity, m_playerBulletParent.transform);
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            // 進化していなくて進化可能であれば
            if (m_level >= LAST_EVOL_NEED_LEVEL)
            {
                Instantiate(m_hardMasPlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
