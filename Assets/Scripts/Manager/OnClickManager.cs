using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickManager : MonoBehaviour
{
    private float panelDistance = 125f;
    private int stagenum;
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
