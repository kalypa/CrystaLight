using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class StageChoiceEnterButton : MonoBehaviour
{
    private RectTransform rect;
    public static Action onClickAction;
    public static Action comeBackAction;
    [SerializeField] private Image fadeBackground;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject equalizer;
    [SerializeField] private GameObject music;
    private void Start() => Init();
    void Init()
    {
        rect = GetComponent<RectTransform>();
        ActionInit();
        BackgroundActiveFalse();
        music.SetActive(false);
    }
    void Update()
    {
        if (fadeBackground.color.a == 1) BackgroundFadeIn();
    }
    void ActionInit()
    {
        onClickAction += ButtonMoveDownEffect;
        onClickAction += BackgroundFadeOut;
        comeBackAction += ButtonMoveUpEffect;
        comeBackAction += BackgroundFadeOut;
    }
    void ButtonMoveDownEffect() => rect.DOAnchorPosY(-590, 0.8f);
    void ButtonMoveUpEffect() => rect.DOAnchorPosY(-490, 0.8f);
    void BackgroundFadeIn()
    {
        fadeBackground.DOFade(0, 2f);
        Invoke(nameof(BackgroundActiveFalse), 2f);
        StageActive();
    }
    void BackgroundFadeOut()
    {
        fadeBackground.gameObject.SetActive(true);
        fadeBackground.DOFade(1, 1f);
    }
    void BackgroundActiveFalse() => fadeBackground.gameObject.SetActive(false);
    void StageActive()
    {
        scrollView.SetActive(true);
        equalizer.SetActive(true);
        music.SetActive(true);
    }
}
