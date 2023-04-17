using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float snapDistance = 1080f;
    public ScrollRect scrollRect;
    private float clampedDistance;
    private float clampedInterval = 1080f;
    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.delta.y;
        float yPos = scrollRect.content.anchoredPosition.y;

        yPos += deltaY;

        clampedDistance = (SongChoiceManager.Instance.songPanelList.Count - 1) * clampedInterval;
        yPos = Mathf.Clamp(yPos, 0, clampedDistance);

        float roundedY = Mathf.Round(yPos / snapDistance) * snapDistance;
        Vector2 newPosition = new Vector2(0, yPos);

        scrollRect.content.anchoredPosition = newPosition;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        float yPos = scrollRect.content.anchoredPosition.y;
        float roundedY = Mathf.Round(yPos / snapDistance) * snapDistance;
        Vector2 snappedPosition = new Vector2(0, roundedY);
        StartCoroutine(SmoothSnap(snappedPosition));
    }

    private IEnumerator SmoothSnap(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // ���� �ִϸ��̼��� ���� �ð��� ������ �� �ֽ��ϴ�.
        Vector2 startPosition = scrollRect.content.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            scrollRect.content.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        scrollRect.content.anchoredPosition = targetPosition;
        SongChoiceManager.Instance.ChoiceSong();
    }

}