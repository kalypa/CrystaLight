using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class OnClickManager : MonoBehaviour
{
    
    public void OnClickStartButton() => SceneManager.LoadScene("TestScene");
    public void OnClickExitButton() => Application.Quit();
    public void OnClickPlayButton() => SceneManager.LoadScene("PlayScene");

    public void OnClickStageChoiceEnterButton()
    {
        StageChoiceEnterButton.onClickAction?.Invoke();
    }
}
