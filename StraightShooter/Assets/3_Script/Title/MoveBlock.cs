// タイトル画面の動くブロック

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    private float m_angle;            // 角度(位置を決める)
    private float m_moveSpeed;        // 移動速度
    private float m_radius;                // 移動する円の半径

    private float ANGLE_MAX = 359.99f;
    private float SPEED_RATE = 0.15f;

    // 各半径倍率
    private float RADIUS_RATE1 = 10.0f;
    private float RADIUS_RATE2 = 20.0f;
    private float RADIUS_RATE3 = 24.0f;
    private float RADIUS_RATE4 = 28.0f;
    private float RADIUS_RATE5 = 32.0f;
    // 追加半径距離
    private float ADD_RADIUS = 5.0f;

    void Start()
    {
        m_angle = Random.value * ANGLE_MAX;
        m_moveSpeed = SPEED_RATE * Random.value; 

        float f = Random.value;

        if (f < 0.2f) m_radius = RADIUS_RATE1 * f;
        else if (f < 0.4f) m_radius = RADIUS_RATE2 * f;
        else if (f < 0.6f) m_radius = RADIUS_RATE3 * f;
        else if (f < 0.8f) m_radius = RADIUS_RATE4 * f;
        else m_radius = RADIUS_RATE5 * f;
        m_radius += ADD_RADIUS;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        m_angle += m_moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Cos(m_angle) * m_radius, transform.position.y, Mathf.Sin(m_angle) * m_radius);
    }
}
