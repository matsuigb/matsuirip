﻿// ベースブロッククラス

using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject m_breakBlock;      // 崩れた時用のオブジェクト

    protected float m_hp;             // 体力
    protected float m_size;           // 大きさ
    protected float m_moveSpeed;      // 移動速度
    protected const float TO_DEATH_TIME = 0.05f;     // 当たってから死ぬまでの時間

    // Instantiateされた時に呼ばれる
    public void Create(float _hp, float _size, float _moveSpeed)
    {
        m_hp = _hp;
        m_size = _size;
        m_moveSpeed = _moveSpeed;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    protected void Move()
    {
        transform.position = new Vector3(
            transform.position.x, transform.position.y - m_moveSpeed, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_breakBlock,transform.position,Quaternion.identity);
        Destroy(gameObject, TO_DEATH_TIME);
    }
}
