// ベースブロック

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    // 体力
    protected float m_hp;

    // 画面外判定
    private const float OUT_WINDOW_X = 10.0f;
    private const float OUT_WINDOW_Y = 10.0f;

    void Start()
    {
    }

    void Update()
    {
        Out_Window_Break();
    }

    protected void Death()
    {
        Destroy(this.gameObject);
    }

    protected void Out_Window_Break()
    {
        // 画面外に出たら消す(上は例外)
        if (this.transform.position.x > OUT_WINDOW_X ||
            this.transform.position.x < -OUT_WINDOW_X ||
            this.transform.position.y < -OUT_WINDOW_Y)
        {
            Destroy(this.gameObject);
        }
    }
}
