// テストエネミー

using UnityEngine;

public class TestEnemy : BaseEnemy
{
    private const float ADD_MOVE_SPEED = 2.0f;
    private Vector3 m_moveVec;
    private float m_moveSpeed;

    void Start()
    {
        m_moveVec = new Vector3(2.0f, 0.5f, 0.0f);
        m_moveSpeed = 1.0f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        m_moveSpeed -= Time.deltaTime * ADD_MOVE_SPEED;
        transform.position += m_moveVec * m_moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(m_moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
