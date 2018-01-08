// ドローン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private float m_cnt;
    private float m_atkCnt;
    private const float DEATH_TIME = 60.0f;

    private const int FIRE_POS_VAL = 4;
    private const float ROTATE_RATE = 15.0f;

    [SerializeField]
    private GameObject m_playerBullet;
    private GameObject m_playerBulletParent;
    [SerializeField]
    private GameObject[] m_firePos;

    void Start()
    {
        m_cnt = 0.0f;
        m_atkCnt = 0.0f;
        m_playerBulletParent = GameObject.Find("PlayerBulletParent");
    }

    void Update()
    {
        m_cnt += Time.deltaTime;
        Fire();
        Rotation();
        Death();
    }

    void Fire()
    {
        m_atkCnt += Time.deltaTime;
        if (m_atkCnt >= 1.0f)
        {
            m_atkCnt = 0.0f;
            // 4つ砲門があるのでその分繰り返す
            for (int i = 0; i < FIRE_POS_VAL; i++)
            {
                GameObject playerBulletObj = Instantiate(m_playerBullet, m_firePos[i].transform.position, Quaternion.identity, m_playerBulletParent.transform);
                PlayerBullet playerBulletComp = playerBulletObj.GetComponent<PlayerBullet>();
                if (playerBulletComp != null)
                {
                    Vector3 directionVec = m_firePos[i].transform.position - this.transform.position;
                    playerBulletComp.Init(directionVec, 1, 1, 1);
                }
                else
                {
                    Debug.Log("playerBulletCompにてnullが発生しています");
                }
            }
        }
    }

    void Rotation()
    {
        this.transform.Rotate(0.0f, 0.0f, Time.deltaTime * ROTATE_RATE);
    }


    void Death()
    {
        if (m_cnt >= DEATH_TIME)
        {
            Destroy(this.gameObject);
        }
    }
}
