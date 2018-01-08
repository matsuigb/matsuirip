using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_normalBlock;
    [SerializeField]
    private GameObject m_twinBlock;
    [SerializeField]
    private GameObject m_enemyBlock;
    [SerializeField]
    private GameObject m_enemyBlock2;
    [SerializeField]
    private GameObject m_enemyBlock3;
    [SerializeField]
    private GameObject m_parentObj;

    private float m_waveCnt = 0.0f;
    const float WAVE_TIME = 30.0f;        // todo

    // ブロック生成
    private float m_cnt = 0.0f;
    private int m_createBlockVal = 3;
    private float CREATE_BLOCK_TIME = 2.0f;

    // 移動
    private float m_moveSpeed = 0.2f;
    private const float MOVE_SPEED_ADD_RATE = 0.1f;
    private const float MAX_MOVE_SPEED = 1.0f;

    bool m_bossFlg = false;

    void Start()
    {
    }

    void Update()
    {
        m_cnt += Time.deltaTime;
        m_waveCnt += Time.deltaTime;

        if (m_cnt > CREATE_BLOCK_TIME)
        {
            m_cnt = 0.0f;
            CreateBlock();
        }
        Move();
    }

    void CreateBlock()
    {
        if (m_waveCnt < WAVE_TIME)
        {
            CreateWave1Block();
        }
        else if (m_waveCnt < WAVE_TIME * 2.0f)
        {
            CreateWave2Block();
            m_createBlockVal = 5;
        }
        else if (m_waveCnt < WAVE_TIME * 3.0f)
        {
            CreateWave3Block();
            m_createBlockVal = 7;
        }
        else
        {
            CreateWave4Block();
            m_createBlockVal = 4;
        }
    }

    void CreateWave1Block()
    {
        Vector3 createPos = new Vector3();
        for (int i = 0; i < m_createBlockVal; i++)
        {
            createPos = new Vector3(Random.value * 10.0f - 10.0f, Random.value + 5.0f, 0.0f);
            float rand = Random.value;

            if (rand < 0.4f)
            {
                Instantiate(m_normalBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.95f)
            {
                Instantiate(m_twinBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else
            {
                Instantiate(m_enemyBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
        }
    }
    void CreateWave2Block()
    {
        Vector3 createPos = new Vector3();
        for (int i = 0; i < m_createBlockVal; i++)
        {
            createPos = new Vector3(Random.value * 10.0f - 10.0f, Random.value + 5.0f, 0.0f);
            float rand = Random.value;

            if (rand < 0.4f)
            {
                Instantiate(m_normalBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.8f)
            {
                Instantiate(m_twinBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.95f)
            {
                Instantiate(m_enemyBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else
            {
                Instantiate(m_enemyBlock2, createPos, Quaternion.identity, m_parentObj.transform);
            }
        }
    }
    void CreateWave3Block()
    {
        Vector3 createPos = new Vector3();
        for (int i = 0; i < m_createBlockVal; i++)
        {
            createPos = new Vector3(Random.value * 10.0f - 10.0f, Random.value + 5.0f, 0.0f);
            float rand = Random.value;

            if (rand < 0.3f)
            {
                Instantiate(m_normalBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.6f)
            {
                Instantiate(m_twinBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.8f)
            {
                Instantiate(m_enemyBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else
            {
                Instantiate(m_enemyBlock2, createPos, Quaternion.identity, m_parentObj.transform);
            }
        }
    }
    void CreateWave4Block()
    {
        Vector3 createPos = new Vector3();
        for (int i = 0; i < m_createBlockVal; i++)
        {
            createPos = new Vector3(Random.value * 10.0f - 10.0f, Random.value + 5.0f, 0.0f);
            float rand = Random.value;

            if (rand < 0.2f)
            {
                Instantiate(m_normalBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.4f)
            {
                Instantiate(m_twinBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else if (rand < 0.6f)
            {
                Instantiate(m_enemyBlock, createPos, Quaternion.identity, m_parentObj.transform);
            }
            else
            {
                Instantiate(m_enemyBlock2, createPos, Quaternion.identity, m_parentObj.transform);
            }
        }

        // 一度だけボス召喚
        if (!m_bossFlg)
        {
            m_bossFlg = true;
            Instantiate(m_enemyBlock3, new Vector3(-3.0f, 7.5f, 0.0f), Quaternion.identity, m_parentObj.transform);
            m_moveSpeed = 0.01f;
        }
    }

    void Move()
    {
        if (m_moveSpeed < MAX_MOVE_SPEED)
        {
            m_moveSpeed += Time.deltaTime * MOVE_SPEED_ADD_RATE;
        }

        this.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + (-m_moveSpeed * Time.deltaTime),
            this.transform.position.z);
    }
}
