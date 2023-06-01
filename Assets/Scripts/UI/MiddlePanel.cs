using UnityEngine;
using DG.Tweening;
using System;
public class MiddlePanel : MonoBehaviour
{
    public Action onClickAction;
    public Action comeBackAction;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject record;
    [SerializeField] GameObject music;
    [SerializeField] GameObject equalizer;
    private RectTransform rect;
    private void Start()
    {
        rect = record.GetComponent<RectTransform>();
        ActionInit();
    }

    void ActionInit()
    {
        onClickAction += EnterInPanel;
        comeBackAction += ExitInPanel;
    }

    void EnterInPanel()
    {
        GameManager.Instance.isInPanel = true;
        panel.SetActive(true);
        PanelCheck(false);
        rect.DOAnchorPosX(200, 0.4f);
    }

    void ExitInPanel()
    {
        GameManager.Instance.isInPanel = false;
        rect.DOAnchorPosX(-200, 0.4f);
        Invoke(nameof(PanelActiveFalse), 0.4f);
    }

    void PanelActiveFalse()
    {
        panel.SetActive(false);
        PanelCheck(true);
    }


    void PanelCheck(bool isActive)
    {
        music.SetActive(isActive);
        equalizer.SetActive(isActive);
    }
}
