using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{

    private int iScore;     //점수
    private int iHp;        //HP
    private int iLevel;     //레벨
    private int iBulletCreateMaxCount;        //일반탄 동시 생성 갯수
    private int iHomingBulletCreateMaxCount;  //유도탄 동시 생성 갯수

    private float fBulletSpeed;               //탄막 속도
    private float fBulletCreateTime;          //일반탄 재생성 시간
    private float fHomingBulletCreateTime;    //유도탄 재생성 시간
    private float fLevelTime;                 //레벨 증가 시간

    private GameObject ScoreUI;
    private GameObject HpUI;
    private GameObject LevelUI;
    private GameObject TankObj;
    public GameObject GameOverUI;
    public GameObject NameField;
    public GameObject explotion;

    public CameraShake shake;

    private bool m_bGameOver;

    public bool IsGameOver { get { return m_bGameOver; } }

    public int GetBulletCreateMaxCount { get { return iBulletCreateMaxCount; } }
    public int GetHomingBulletCreateMaxCount { get { return iHomingBulletCreateMaxCount; } }

    public float GetBulletSpeed { get { return fBulletSpeed; } }
    public float GetBulletCreateTime { get { return fBulletCreateTime; } }
    public float GetHomingBulletCreateTime { get { return fHomingBulletCreateTime; } }

    void Awake()
    {
        ScoreUI = GameObject.Find("Score");
        HpUI = GameObject.Find("Hp");
        LevelUI = GameObject.Find("Level");
        TankObj = GameObject.Find("Tank");
        shake = TankObj.GetComponent<CameraShake>();
        iScore = 0;
        iHp = 5;
        iLevel = 1;

        iBulletCreateMaxCount = 1;
        iHomingBulletCreateMaxCount = 0;

        fBulletSpeed = -7.5f;
        fBulletCreateTime = 1.0f;
        fHomingBulletCreateTime = 3.0f;
        fLevelTime = 0.0f;

        ScoreUI.GetComponent<Text>().text = "Score:" + iScore;
        HpUI.GetComponent<Text>().text = "Hp:" + iHp;
        LevelUI.GetComponent<Text>().text = "Level:" + iLevel;
        m_bGameOver = false;
    }

    void Update()
    {
        if (GameOver())
            LevelUp();
        else
            NameCheck();
    }

    public void LevelUp()
    {
        fLevelTime += Time.deltaTime;
        if (fLevelTime >= 5.0f)
        {
            iLevel++;
            fBulletSpeed *= 1.1f;
            fBulletCreateTime *= 0.9f;
            if (iLevel > 4)
                fHomingBulletCreateTime *= 0.9f;
            if (iLevel % 2 == 0)
                iBulletCreateMaxCount++;
            if (iLevel % 3 == 0)
                iHomingBulletCreateMaxCount++;

            LevelUI.GetComponent<Text>().text = "Level:" + iLevel;
            fLevelTime = 0.0f;
        }
    }

    public void IncreassScore()
    {
        iScore += iLevel;
        ScoreUI.GetComponent<Text>().text = "Score:" + iScore;
    }

    public void HpMinus()
    {
        if (!shake.IsEnd)
        {
            iHp--;
            HpUI.GetComponent<Text>().text = "Hp:" + iHp;
            shake.IsEnd = true;
            StartCoroutine(shake.ShakeCamera(0.5f, 0.3f));
        }

    }

    bool GameOver()
    {
        if (iHp <= 0 && TankObj != null)
        {
            GameObject go = Instantiate(explotion, TankObj.transform.position, TankObj.transform.rotation) as GameObject;
            ParticleSystem ExplosionParticles = go.GetComponent<ParticleSystem>();
            ExplosionParticles.Play();
            ParticleSystem.Destroy(go, ExplosionParticles.main.simulationSpeed);
            Destroy(TankObj);
            m_bGameOver = true;
            HpUI.gameObject.SetActive(false);
            NameField.gameObject.SetActive(true);
            GameOverUI.gameObject.SetActive(true);
            TankObj = null;
            return false;
        }
        else if (TankObj == null)
            return false;
        return true;
    }

    void NameCheck()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            name = NameField.GetComponent<InputField>().text;
            NameField.GetComponent<Rank>().NameCheck(name, iScore, NameField);
        }
    }

    bool StringNullCheck(string s)
    {
        if (s == null)
            return true;
        return false;
    }
}
