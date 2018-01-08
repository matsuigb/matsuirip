using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock2 : BaseBlock
{

    // 経験値
    private float m_exp = 150.0f;

    [SerializeField]
    private GameObject m_playerObj;

    [SerializeField]
    private GameObject m_breakEffect;

    private float m_speed = 0.3f;

    private GameObject m_playerInfo;
    private PlayerInfo m_playerInfoComp;

    private float m_RotateRate;

    // 攻撃関連
    [SerializeField]
    private GameObject m_miniBlock;
    [SerializeField]
    private GameObject[] m_firePos;
    private GameObject m_stage;
    private float m_atkCnt = 0.0f;
    const int FIRE_POS_VAL = 8;

    void Start()
    {
        m_RotateRate = Random.value * 10.0f + 20.0f;
        m_hp = 40.0f;
        m_playerInfo = GameObject.FindWithTag("PlayerInfo");
        m_stage = GameObject.Find("Stage");
    }

    void Update()
    {
        Move();
        Fire();
        Out_Window_Break();        // 画面外対策
    }

    void Move()
    {
        Vector3 vec = new Vector3();

        m_playerInfoComp = m_playerInfo.GetComponent<PlayerInfo>();
        if (m_playerInfoComp != null)
        {
            vec = m_playerInfoComp.playerPos - this.transform.position;
        }
        vec.z = 0.0f;
        vec.Normalize();

        this.transform.position += new Vector3(vec.x * Time.deltaTime, vec.y * Time.deltaTime, 0.0f);

        // 向き制御
        this.transform.Rotate(0.0f, 0.0f, Time.deltaTime * m_RotateRate);
    }

    void Fire()
    {
        m_atkCnt += Time.deltaTime;
        if (m_atkCnt >= 0.5f)
        {
            m_atkCnt = 0.0f;
            // 8つ砲門があるのでその分繰り返す
            for (int i = 0; i < FIRE_POS_VAL; i++)
            {
                GameObject miniBlockObj = Instantiate(m_miniBlock, m_firePos[i].transform.position, Quaternion.identity, m_stage.transform);
                MiniBlock miniBlockComp = miniBlockObj.GetComponent<MiniBlock>();
                if (miniBlockComp != null)
                {
                    Vector3 directionVec = m_firePos[i].transform.position - this.transform.position;
                    miniBlockComp.Init(directionVec);
                }
                else
                {
                    Debug.Log("playerBulletCompにてnullが発生しています");
                }
            }
        }
    }

    // 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet playerBullet = collision.transform.GetComponent<PlayerBullet>();
            m_hp -= playerBullet.damage;

            // 慣性(はじけとび)は現状なし
            //Vector3 vec = this.transform.position - collision.transform.position;
            //this.GetComponent<Rigidbody>().AddForce(vec.x * 200.0f, vec.y * 200.0f, 0.0f);

            // エネミーはプレイヤーの弾を消化する
            Destroy(collision.gameObject);

            if (m_hp <= 0)
            {
                AddPlayerExp();
                Death();
                Instantiate(m_breakEffect, this.transform.position, Quaternion.identity);
            }
        }
    }

    private void AddPlayerExp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.SendMessage("AddExp", m_exp);
    }
}
