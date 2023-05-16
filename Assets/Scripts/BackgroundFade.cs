using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFade : MonoBehaviour
{
    [SerializeField] private Image[] background;
    private float panelDistance = 1080f;
    private float prevContentPosition;
    public ScrollRect scrollRect;

    public void BackgroundFadeEffect()
    {
        float contentPosition = scrollRect.content.anchoredPosition.y;
        int idx = Mathf.FloorToInt(contentPosition / panelDistance);

        idx = Mathf.Clamp(idx, 0, background.Length - 1);

        float normalizedPosition = 0f;
        if (contentPosition >= idx * panelDistance && contentPosition < (idx + 1) * panelDistance)
        {
            normalizedPosition = (contentPosition - idx * panelDistance) / panelDistance;
        }
        else
        {
            normalizedPosition = 0f;
        }

        Color color = background[idx].color;
        color.a = 1 - normalizedPosition;
        background[idx].color = color;

        prevContentPosition = contentPosition;
    }
}
