using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using TMPro;

public class OnClickManager : MonoBehaviour
{
    [SerializeField] private StageChoiceEnterButton enterButton;
    [SerializeField] private MiddlePanel middlePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject countText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI offsetText;
    public void OnClickExitButton() => Application.Quit();

    public void OnClickStageChoiceEnterButton() => enterButton.onClickAction?.Invoke();
    public void OnClickStageChoiceExitButton()
    {
        if(GameManager.Instance.isInPanel) middlePanel.comeBackAction?.Invoke();
        else enterButton.comeBackAction?.Invoke();
    }

    public void OnClickStagePlayButton() => middlePanel.onClickAction?.Invoke();

    public void OnClickGameStartButton() => startPanel.SetActive(false);

    public void OnClickContinueButton() => ContinueGame();
    public void OnClickGameStopButton()
    {
        Time.timeScale = 0;
        stopPanel.SetActive(true);
    }

    public void OnClickSettingButton() => settingPanel.SetActive(true);
    public void OnClickSettingExitButton() => settingPanel.SetActive(false);
    public void OnClickGameExitButton()
    {
        settingPanel.SetActive(false);
        exitPanel.SetActive(true);
    }
    public void ExitAgreeButton() => Application.Quit();
    public void ExitDegreeButton() => exitPanel.SetActive(false);

    public void OffsetPlusButton() => GameManager.Instance.offset += 1;
    public void OffsetMinusButton() => GameManager.Instance.offset -= 1;

    public void OnClickSoundZero() => volumeSlider.value = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
        if(offsetText != null) offsetText.text = GameManager.Instance.offset.ToString();
    }

    void ContinueGame()
    {
        stopPanel.SetActive(false);
        countText.SetActive(true);
    }
}
