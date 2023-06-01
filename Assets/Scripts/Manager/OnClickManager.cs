using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class OnClickManager : MonoBehaviour
{
    [SerializeField] private StageChoiceEnterButton enterButton;
    [SerializeField] private MiddlePanel middlePanel;
    public void OnClickExitButton() => Application.Quit();

    public void OnClickStageChoiceEnterButton() => enterButton.onClickAction?.Invoke();
    public void OnClickStageChoiceExitButton()
    {
        if(GameManager.Instance.isInPanel) middlePanel.comeBackAction?.Invoke();
        else enterButton.comeBackAction?.Invoke();
    }

    public void OnClickStagePlayButton() => middlePanel.onClickAction?.Invoke();
}
