// ノーマルバレットクラス

using UnityEngine;

public class NormalBullet : BaseBullet
{
    void Start()
    {
    }

    void Update()
    {
        Move();
        CheckDeath();
    }

    void Move()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + m_moveSpeed * Time.deltaTime,
            transform.position.z);
    }
}
