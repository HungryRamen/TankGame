using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Rank : MonoBehaviour
{
    private GameObject RankNameUI;
    private GameObject RankScoreUI;
    private string path; //저장 경로
    struct SRank
    {
        private string sName;
        private int iScore;
        public string Name { get { return sName; } set { sName = value; } }
        public int Score { get { return iScore; } set { iScore = value; } }
    }

    void Awake()
    {
        path = Application.persistentDataPath + "/Rank.txt";
        RankNameUI = GameObject.Find("RankName");
        RankScoreUI = GameObject.Find("RankScore");
        if (RankNameUI != null)
            RankView();
    }

    void RankView()
    {
        if (File.Exists(path))
        {
            StreamReader Read = File.OpenText(path);
            SRank[] r = new SRank[5];
            string Input = "";
            int i = 0;
            while (true)
            {
                Input = Read.ReadLine();
                if (StringNullCheck(Input)) { break; }
                r[i].Name = Input;
                Input = Read.ReadLine();
                if (StringNullCheck(Input)) { break; }
                r[i].Score = Convert.ToInt32(Input);
                i++;
            }
            Read.Close();
            string sName = "";
            string sScore = "";
            for (int j = 0; j < i; j++)
            {
                sName = sName + "이름:" + r[j].Name + "\n";
                sScore = sScore + "점수:" + r[j].Score + "\n";
            }
            RankNameUI.GetComponent<Text>().text = sName;
            RankScoreUI.GetComponent<Text>().text = sScore;
        }
    }

    public void NameCheck(string name, int iScore, GameObject NameField)
    {
        if (!File.Exists(path))
        {
            StreamWriter Write = new StreamWriter(path);
            Write.WriteLine(name);
            Write.WriteLine(iScore);
            Write.Flush();
            Write.Close();
        }
        else
        {
            StreamReader Read = File.OpenText(path);
            SRank[] r = new SRank[5];
            string Input = "";
            int i = 0;
            while (true)
            {
                Input = Read.ReadLine();
                if (StringNullCheck(Input)) { break; }
                r[i].Name = Input;
                Input = Read.ReadLine();
                if (StringNullCheck(Input)) { break; }
                r[i].Score = Convert.ToInt32(Input);
                i++;
            }
            bool bCheck = false;
            for (int j = 0; j < i; j++)
            {
                if (r[j].Score < iScore)
                {
                    if (i >= r.Length)
                    {
                        i--;
                    }
                    for (int k = i; k > j; k--)
                    {
                        r[k].Name = r[k - 1].Name;
                        r[k].Score = r[k - 1].Score;
                    }
                    r[j].Name = name;
                    r[j].Score = iScore;
                    bCheck = true;
                    if (i < 5)
                    {
                        i++;
                    }
                    break;
                }
            }
            if (!bCheck)
            {
                if (i < 5)
                {
                    r[i].Name = name;
                    r[i].Score = iScore;
                    i++;
                }
            }
            Read.Close();
            StreamWriter Write = new StreamWriter(path);
            for (int j = 0; j < i; j++)
            {
                Write.WriteLine(r[j].Name);
                Write.WriteLine(r[j].Score);
            }
            Write.Flush();
            Write.Close();
        }
        SceneManager.LoadScene("MainScene");
    }

    bool StringNullCheck(string s)
    {
        if (s == null)
            return true;
        return false;
    }
}
