using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class StageChoiceEnterButton : MonoBehaviour
{
    private RectTransform rect;
    public Action onClickAction;
    public Action comeBackAction;
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
        if (fadeBackground.color.a == 1)
        {
            if(scrollView.activeSelf) BackgroundFadeIn(false);
            else BackgroundFadeIn(true);
        }
    }
    void ActionInit()
    {
        onClickAction += ButtonMoveDownEffect;
        onClickAction += BackgroundFadeOut;
        comeBackAction += BackgroundFadeOut;
    }
    void ButtonMoveDownEffect() => rect.DOAnchorPosY(-590, 0.5f);
    void ButtonMoveUpEffect() => rect.DOAnchorPosY(-490, 0.5f);
    void BackgroundFadeIn(bool active)
    {
        fadeBackground.DOFade(0, 0.7f);
        Invoke(nameof(BackgroundActiveFalse), 0.7f);
        StageActive(active);
    }
    void BackgroundFadeOut()
    {
        fadeBackground.gameObject.SetActive(true);
        fadeBackground.DOFade(1, 0.7f);
    }
    void BackgroundActiveFalse() => fadeBackground.gameObject.SetActive(false);
    void StageActive(bool active)
    {
        scrollView.SetActive(active);
        equalizer.SetActive(active);
        music.SetActive(active);
        if (active == false) ButtonMoveUpEffect();
    }
}
