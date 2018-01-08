// プレイヤーバレット

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{

    private const float HP_INIT_VALUE = 3.0f;
    private const float SPEED_INIT_VALUE = 2.0f;
    private const float DAMAGE_INIT_VALUE = 3.0f;

    // 生成されたときに呼ばれる初期化関数
    public void Init(Vector3 _directionVec, int _hpLevel, int _speedLevel, int _damageLevel)
    {
        _directionVec.z = 0.0f;
        _directionVec.Normalize();
        m_directionVec = _directionVec;

        m_hp = HP_INIT_VALUE + _hpLevel;
        m_speed = SPEED_INIT_VALUE + _speedLevel;
        damage = DAMAGE_INIT_VALUE + _damageLevel;
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
}
