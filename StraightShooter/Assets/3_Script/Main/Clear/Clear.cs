﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    bool m_clearFlg = false;
    private float m_alpha=0.0f;

    void Start()
    {
        GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, m_alpha);
    }

    void Update()
    {
        if (m_clearFlg)
        {
            m_alpha += (Time.deltaTime/2.0f);
            GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, m_alpha);
        }

        if(m_alpha >= 3.0f)
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void OnClearFlg()
    {
        m_clearFlg = true;
    }
}
