using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBlock : BaseBullet
{

    private const float HP_INIT_VALUE = 1.0f;
    private const float SPEED_INIT_VALUE = 4.0f;
    private const float DAMAGE_INIT_VALUE = 1.0f;

    // 生成されたときに呼ばれる初期化関数
    public void Init(Vector3 _directionVec)
    {
        _directionVec.z = 0.0f;
        _directionVec.Normalize();
        m_directionVec = _directionVec;

        m_hp = HP_INIT_VALUE;
        m_speed = SPEED_INIT_VALUE;
        damage = DAMAGE_INIT_VALUE;
    }

    void Start()
    {
    }

    void Update()
    {
        Move();
        Break();
    }

    private void Move()
    {
        float addX = m_directionVec.x * Time.deltaTime * m_speed;
        float addY = m_directionVec.y * Time.deltaTime * m_speed;
        this.transform.position += new Vector3(addX, addY, 0.0f);
    }

    // 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("PlayerBullet"))
        //{
            Destroy(this.gameObject);
        //}
    }
}
