using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class OnClickManager : MonoBehaviour
{
    [SerializeField] private StageChoiceEnterButton enterButton;
    public void OnClickStartButton() => SceneManager.LoadScene("TestScene");
    public void OnClickExitButton() => Application.Quit();
    public void OnClickPlayButton() => SceneManager.LoadScene("PlayScene");

    public void OnClickStageChoiceEnterButton() => enterButton.onClickAction?.Invoke();
    public void OnClickStageChoiceExitButton() => enterButton.comeBackAction?.Invoke();

    public void OnClickStageRetryButton() => SceneManager.LoadScene("PlayScene");
    public void OnClickGoBackButton() => SceneManager.LoadScene("LobbyScene");
}
