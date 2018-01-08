using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : BaseBlock
{

    // 経験値
    private float m_exp = 5.0f;

    void Start()
    {
        m_hp = 5.0f;
    }

    void Update()
    {
        Out_Window_Break();        // 画面外対策
    }

    // 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet playerBullet = collision.transform.GetComponent<PlayerBullet>();
            m_hp -= playerBullet.damage;

            Vector3 vec = this.transform.position - collision.transform.position;
            this.GetComponent<Rigidbody>().AddForce(vec.x * 200.0f, vec.y * 200.0f, 0.0f);

            if (m_hp <= 0)
            {
                AddPlayerExp();
                Death();
            }
        }
    }

    private void AddPlayerExp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.SendMessage("AddExp", m_exp);
    }
}
