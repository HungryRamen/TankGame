using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnRankClick()
    {
        SceneManager.LoadScene("RankScene");
    }
    public void OnEscClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
    public void OnHomeClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
