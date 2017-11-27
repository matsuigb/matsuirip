// ブロックマネージャ

using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_block;      // ブロックオブジェクト
    [SerializeField]
    private GameObject m_blocks;    // ブロックオブジェクト

    // ブロック生成系変数
    private float m_cnt;
    private float m_createBlockTime;
    private Vector3 m_createPos;
    // ブロック系変数
    private float m_blockHP;
    private float m_blockSize;
    private float m_blockMoveSpeed;

    private const float BLOCK_SPACE = 2.0f;      // ブロックの間隔
    private const float BLOCK_X_POS = -7.0f;      // ブロックのX生成場所
    private const int BLOCK_MAX_X = 8;      // ブロック横生成数

    void Start()
    {
        // ブロック生成系変数
        m_cnt = 99999.0f;
        m_createBlockTime = 10.0f;
        m_createPos = new Vector3(0.0f, 12.0f, 0.0f);
        // ブロック系変数
        m_blockHP = 1.0f;
        m_blockSize = 1.0f;
        m_blockMoveSpeed = 0.03f;
    }

    void Update()
    {
        //CreateBlock();        // 昔の実践方法のためいったん消す
        CreateBlocks();     // ブロックはゲームオブジェクトとしてパターン毎にまとめておいて、それを生成する
    }

    void CreateBlocks()
    {
        m_cnt += Time.deltaTime;
        if (m_cnt > m_createBlockTime)
        {
            m_cnt = 0.0f;
            Instantiate(m_blocks, m_createPos, Quaternion.identity);
        }
    }

    void CreateBlock()
    {
        m_cnt += Time.deltaTime;
        if (m_cnt > m_createBlockTime)
        {
            m_cnt = 0.0f;
            for (int i = 0; i < BLOCK_MAX_X; i++)
            {
                GameObject obj = Instantiate(m_block, m_createPos + new Vector3(BLOCK_SPACE * i + BLOCK_X_POS, 0, 0), Quaternion.identity);
                obj.GetComponent<NormalBlock>().Create(1.0f, 1.0f, m_blockMoveSpeed);
            }
        }
    }
}
