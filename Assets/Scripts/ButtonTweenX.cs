using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using static UnityEngine.UI.Image;

public class ButtonTweenX : MonoBehaviour
{
    private RectTransform rect;
    private float moveDistance = 1f;
    private float moveDuration = 0.1f;
    private int loops = -1;
    private LoopType loopType = LoopType.Yoyo;
    private float xDelta = 0.2f;
    private float xMoveSpeed = 2;
    private void Update()
    {
        TweeningButtonX();
    }
    void TweeningButtonX()
    {
        rect = GetComponent<RectTransform>();
        float xStartPosition = rect.anchoredPosition.x;
        float x = xStartPosition + xDelta * Mathf.Sin(Time.time * xMoveSpeed);
        rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
    }
}
