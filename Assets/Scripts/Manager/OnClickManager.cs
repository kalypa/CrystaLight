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
    public void OnClickSongPanel1()
    {
        stagenum = 0;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickSongPanel2()
    {
        stagenum = 1;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickSongPanel3()
    {
        stagenum = 2;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickSongPanel4()
    {
        stagenum = 3;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickSongPanel5()
    {
        stagenum = 4;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickSongPanel6()
    {
        stagenum = 5;
        SongChoiceManager.Instance.scrollRect.content.anchoredPosition = new Vector2(0, panelDistance * stagenum);
    }
    public void OnClickStageStartButton()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
