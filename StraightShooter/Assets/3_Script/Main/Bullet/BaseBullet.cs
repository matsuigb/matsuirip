// ベースバレッド

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{

    // 体力(貫通力)
    protected float m_hp;
    // 速度
    protected float m_speed;
    // ダメージ
    public float damage;

    protected Vector3 m_directionVec;     // 向いている方向(進行方向)

    // 画面外判定
    private const float OUT_WINDOW_X = 10.0f;
    private const float OUT_WINDOW_Y = 6.0f;

    void Start()
    {
    }

    void Update()
    {
    }

    protected void Break()
    {
        // 画面外に出たら消す
        if (this.transform.position.x > OUT_WINDOW_X || 
            this.transform.position.x < -OUT_WINDOW_X ||
            this.transform.position.y > OUT_WINDOW_Y ||
            this.transform.position.y < -OUT_WINDOW_Y)
        {
            Destroy(this.gameObject);
        }
    }
}
