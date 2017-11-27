// アライトメントブロッククラス
// ブロックを纏めて動かすオブジェクトのスクリプト(仮)

using UnityEngine;

public class AlignmentBlock1 : MonoBehaviour
{

    private float m_moveSpeed;

    void Start()
    {
        m_moveSpeed = 1.0f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += new Vector3(0.0f, -m_moveSpeed * Time.deltaTime, 0.0f);
    }
}
