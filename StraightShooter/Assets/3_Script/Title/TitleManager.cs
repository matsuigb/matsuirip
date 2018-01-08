// タイトルマネージャ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_moveBlock;
    [SerializeField]
    private GameObject m_moveBlock2;
    [SerializeField]
    private GameObject m_parentObj;

    private AudioSource m_se;
    bool m_sceneChangeFlg = false;
    float m_cnt;
    const float CHANGE_SCENE_TIME = 3.5f;

    private const int MAKE_BLOCK_VAL = 512;

    void Start()
    {
        m_cnt = 0.0f;
        m_se = GetComponent<AudioSource>();

        for (int i = 0; i < MAKE_BLOCK_VAL; i++)
        {
            if (i < MAKE_BLOCK_VAL/2)
            {
                Instantiate(m_moveBlock, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, m_parentObj.transform);
            }
            else
            {
                Instantiate(m_moveBlock2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, m_parentObj.transform);
            }
        }
    }

    void Update()
    {
        // シーン遷移
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_sceneChangeFlg = true;
            m_se.PlayOneShot(m_se.clip);
        }

        if (m_sceneChangeFlg)
        {
            m_cnt += Time.deltaTime;
            if(m_cnt>= CHANGE_SCENE_TIME)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}
