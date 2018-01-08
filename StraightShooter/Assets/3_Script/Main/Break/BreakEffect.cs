// ブレイクブロッククラス

using UnityEngine;

public class BreakEffect : MonoBehaviour
{
    private float m_cnt;
    public const float DEATH_TIME = 0.5f;

    void Start()
    {
        m_cnt = 0.0f;
    }

    void Update()
    {
        m_cnt += Time.deltaTime;
        if (m_cnt >= DEATH_TIME)
        {
            Death();
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
