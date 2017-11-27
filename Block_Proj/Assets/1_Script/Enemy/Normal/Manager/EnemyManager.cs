// エネミーマネージャ

using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;

    private float m_cnt;
    private const float CREATE_ENEMY_TIME = 5.0f;

    private Vector3 createEnemyPos;

    void Start()
    {
        m_cnt = 0.0f;
        createEnemyPos = new Vector3(5.0f, 8.0f, 0.0f);
    }

    void Update()
    {
        m_cnt += Time.deltaTime;
        CreateEnemy();
    }

    void CreateEnemy()
    {
        if (m_cnt >= CREATE_ENEMY_TIME)
        {
            Instantiate(m_enemy, createEnemyPos, Quaternion.identity);
            m_cnt = 0.0f;
        }
    }
}
