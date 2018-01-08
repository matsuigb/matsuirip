// ベースプレイヤー

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    protected AudioSource m_fireSE;

    [SerializeField]
    private GameObject m_breakEffect;

    // 各オブジェクト
    [SerializeField]
    protected GameObject m_playerBullet;
    [SerializeField]
    protected GameObject m_playerBulletParent;
    [SerializeField]
    private GameObject m_firePos;
    [SerializeField]
    private GameObject m_droneObj;

    [SerializeField]
    private GameObject m_uiIcon;
    //[SerializeField]
    //private GameObject[] m_uiIconPos;

    // 各項目の最大レベル
    private const int MAX_LEVEL_VAL = 8;

    [SerializeField]
    private GameObject m_twinPlayer;
    [SerializeField]
    private GameObject m_sniperPlayer;
    [SerializeField]
    private GameObject m_masPlayer;

    protected GameObject m_playerInfo;
    protected PlayerInfo m_playerInfoComp;

    // レベル
    protected int m_level = 1;                // todo
    // 経験値
    private float m_exp = 50.0f;       // todo
    private float m_nextNeedExp = 10.0f;
    private const float NEXT_EXP_RATE = 1.1f;
    // 進化
    private bool m_evolFlg = false;        // 進化済かどうか
    private int EVOL_NEED_LEVEL = 10;        // 進化に必要なレベル
    protected int LAST_EVOL_NEED_LEVEL = 15;        // 最終進化に必要なレベル

    // 体力
    private float m_maxHP = 10.0f;
    private float m_hp = 10.0f;
    private int m_hpLevel = 1;
    private const float LEVEL_UP_HP = 1.0f;
    // 速度
    private float m_speed = 1.0f;
    private int m_speedLevel = 1;
    private const float LEVEL_UP_SPEED = 0.5f;
    // リロード速度
    protected float m_reloadTime = 1.0f;
    private int m_reloadTimeLevel = 1;
    private const float LEVEL_UP_RELOAD = 0.8f;
    protected float m_atkCnt = 0.0f;      // 攻撃用カウンター
    // 回復力
    private float m_healPower = 1.0f;
    private int m_healPowerLevel = 1;
    private const float LEVEL_UP_HEAL = 0.15f;
    private float m_healCnt = 0.0f;      // 回復用カウンター
    // ドローン(固定砲台)
    private float m_dronePowerLevel = 1;
    // 弾の貫通力(体力)
    protected int m_bulletHPLevel = 1;
    // 弾の速度
    protected int m_bulletSpeedLevel = 1;
    // 弾の攻撃力
    protected int m_bulletDamageLevel = 1;

    // 移動限界
    const float MAX_X = 2.1f;
    const float MIN_X = -8.5f;
    const float MAX_Y = 4.8f;
    const float MIN_Y = -4.8f;

    void Start()
    {
        m_playerInfo = GameObject.FindWithTag("PlayerInfo");
        m_fireSE = GetComponent<AudioSource>();
    }

    void Update()
    {
        PowerUp();
        Move();
        Attack();
        HEAL();
        Evolution();
    }

    // ----------------------------------------------------------------- //
    /////////////////////////// パワーアップ系 ///////////////////////////
    // ----------------------------------------------------------------- //
    protected void PowerUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && m_hpLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_hpLevel++;
                m_maxHP += LEVEL_UP_HP;
                CreateUIIcon(1, m_hpLevel);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && m_speedLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_speedLevel++;
                m_speed += LEVEL_UP_SPEED;
                CreateUIIcon(2, m_speedLevel);

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && m_reloadTimeLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_reloadTimeLevel++;
                m_reloadTime *= LEVEL_UP_RELOAD;        // 乗算なので注意
                CreateUIIcon(3, m_reloadTimeLevel);

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && m_healPowerLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_healPowerLevel++;
                m_healPower += LEVEL_UP_HEAL;
                CreateUIIcon(4, m_healPowerLevel);

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_dronePowerLevel++;
                Instantiate(m_droneObj, this.transform.position, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && m_bulletHPLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_bulletHPLevel++;
                CreateUIIcon(5, m_bulletHPLevel);

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && m_bulletSpeedLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_bulletSpeedLevel++;
                CreateUIIcon(6, m_bulletSpeedLevel);

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && m_bulletDamageLevel < MAX_LEVEL_VAL)
        {
            bool levelUp = LevelUp();
            if (levelUp)
            {
                m_bulletDamageLevel++;
                CreateUIIcon(7, m_bulletDamageLevel);

            }
        }
    }

    private void CreateUIIcon(int heightNum, int widthNum)
    {
        // todo 直値
        float h = (heightNum - 1) * 0.5f;
        float w = widthNum * 0.4f;

        Instantiate(m_uiIcon, new Vector3(5.1f + w, 4.1f - h, 0.0f), Quaternion.identity);
    }

    private bool LevelUp()
    {
        // 消費できるだけの経験値があるか確認
        if (m_exp >= m_nextNeedExp)
        {
            m_level++;
            m_exp -= m_nextNeedExp;     // 経験値を消費
            m_nextNeedExp *= NEXT_EXP_RATE;     // 次の経験値に必要な量を乗算
            return true;
        }
        return false;
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 移動系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            UpMove();
        }
        if (Input.GetKey(KeyCode.A))
        {
            LeftMove();
        }
        if (Input.GetKey(KeyCode.S))
        {
            DownMove();
        }
        if (Input.GetKey(KeyCode.D))
        {
            RightMove();
        }
        // 向きを設定
        Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - cameraWorldpos);
        transform.localRotation = rotation;

        // PlayerInfoに教えてあげる
        m_playerInfoComp = m_playerInfo.GetComponent<PlayerInfo>();
        if (m_playerInfoComp != null)
        {
            m_playerInfoComp.playerPos = this.transform.position;
        }

        // はみ出したところは戻す関数
        MoveInWindow();
    }

    private void MoveInWindow()
    {
        if (this.transform.position.x > MAX_X)
        {
            this.transform.position = new Vector3(MAX_X, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < MIN_X)
        {
            this.transform.position = new Vector3(MIN_X, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y > MAX_Y)
        {
            this.transform.position = new Vector3(this.transform.position.x, MAX_Y, this.transform.position.z);
        }
        else if (this.transform.position.y < MIN_Y)
        {
            this.transform.position = new Vector3(this.transform.position.x, MIN_Y, this.transform.position.z);
        }
    }

    private void RightMove()
    {
        float addSpeed = Time.deltaTime * m_speed;
        this.transform.position = new Vector3(this.transform.position.x + addSpeed, this.transform.position.y, this.transform.position.z);
    }
    private void LeftMove()
    {
        float addSpeed = Time.deltaTime * m_speed;
        this.transform.position = new Vector3(this.transform.position.x - addSpeed, this.transform.position.y, this.transform.position.z);
    }
    private void UpMove()
    {
        float addSpeed = Time.deltaTime * m_speed;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + addSpeed, this.transform.position.z);
    }
    private void DownMove()
    {
        float addSpeed = Time.deltaTime * m_speed;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - addSpeed, this.transform.position.z);
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 攻撃系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected virtual void Attack()
    {
        m_atkCnt += Time.deltaTime;
        if (Input.GetMouseButton(0) && m_atkCnt >= m_reloadTime)
        {
            m_fireSE.PlayOneShot(m_fireSE.clip);

            m_atkCnt = 0.0f;
            Vector3 cameraWorldpos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Vector3 directionVec = Input.mousePosition - cameraWorldpos;

            GameObject playerBulletObj = Instantiate(m_playerBullet, m_firePos.transform.position, Quaternion.identity, m_playerBulletParent.transform);
            PlayerBullet playerBulletComp = playerBulletObj.GetComponent<PlayerBullet>();
            if (playerBulletComp != null)
            {
                playerBulletComp.Init(directionVec, m_bulletHPLevel, m_bulletSpeedLevel, m_bulletDamageLevel);
            }
            else
            {
                Debug.Log("playerBulletCompにてnullが発生しています");
            }
        }
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 回復系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected void HEAL()
    {
        m_healCnt += Time.deltaTime;
        if (m_healCnt >= 1.0f)
        {
            m_healCnt = 0.0f;
            m_hp += m_healPower;
        }
        // 体力は限界値を超えない
        if (m_hp > m_maxHP)
        {
            m_hp = m_maxHP;
        }
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 進化系 //////////////////////////////
    // ----------------------------------------------------------------- //
    protected virtual void Evolution()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 進化していなくて進化可能であれば
            if (!m_evolFlg && m_level >= EVOL_NEED_LEVEL)
            {
                Instantiate(m_twinPlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            // 進化していなくて進化可能であれば
            if (!m_evolFlg && m_level >= EVOL_NEED_LEVEL)
            {
                Instantiate(m_sniperPlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            // 進化していなくて進化可能であれば
            if (!m_evolFlg && m_level >= EVOL_NEED_LEVEL)
            {
                Instantiate(m_masPlayer, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 被弾系 //////////////////////////////
    // ----------------------------------------------------------------- //
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            this.m_hp -= 1.0f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BossBlock"))
        {
            this.m_hp -= 999.0f;
        }

        if (m_hp <= 0.0f)
        {
            Death();
        }
    }

    void Death()
    {
        // プレイヤー死亡はゲームオーバー
        GameObject over = GameObject.FindWithTag("Over");
        over.GetComponent<Over>().OnOverFlg();
    }

    // ----------------------------------------------------------------- //
    ////////////////////////////// 外部系 //////////////////////////////
    // ----------------------------------------------------------------- //
    public void AddExp(float _addPoint)
    {
        m_exp += _addPoint;
    }
}
