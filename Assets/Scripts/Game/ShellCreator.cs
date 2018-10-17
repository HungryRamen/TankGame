using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCreator : MonoBehaviour
{

    float fCreateTime1; //일반탄 재생성 시간
    float fCreateTime2; //유도탄 재생성 시간
    float fZ;           //임의의 위치
    public GameObject ShellObj1;
    public GameObject ShellObj2;
    private GameObject UI;
    void Awake()
    {
        UI = GameObject.Find("Canvas");
        fCreateTime1 = UI.GetComponent<GameMain>().GetBulletCreateTime;
        fCreateTime2 = UI.GetComponent<GameMain>().GetHomingBulletCreateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UI.GetComponent<GameMain>().IsGameOver)
        {
            fCreateTime1 += Time.deltaTime;
            fCreateTime2 += Time.deltaTime;
            if (fCreateTime1 >= UI.GetComponent<GameMain>().GetBulletCreateTime)
            {
                int rand = Random.Range(1, UI.GetComponent<GameMain>().GetBulletCreateMaxCount); //생성 갯수 랜덤 최소 1 ~ 최대 MAX
                for (int i = 0; i < rand; i++)
                {
                    fZ = Random.Range(-20.0f, 20.0f);
                    Instantiate(ShellObj1, new Vector3(this.transform.position.x, this.transform.position.y, fZ), this.transform.rotation);
                    fCreateTime1 = 0.0f;
                    UI.GetComponent<GameMain>().IncreassScore();
                }
            }
            if (fCreateTime2 >= UI.GetComponent<GameMain>().GetHomingBulletCreateTime)
            {
                for (int i = 0; i < UI.GetComponent<GameMain>().GetHomingBulletCreateMaxCount; i++)
                {
                    fZ = Random.Range(-20.0f, 20.0f);
                    Instantiate(ShellObj2, new Vector3(this.transform.position.x, this.transform.position.y, fZ), this.transform.rotation);
                    fCreateTime2 = 0.0f;
                    UI.GetComponent<GameMain>().IncreassScore();
                }
            }
        }
    }
}
