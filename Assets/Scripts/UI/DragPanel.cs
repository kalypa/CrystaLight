using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button playButton;
    public float snapDistance = 1080f;
    public ScrollRect scrollRect;
    private float clampedDistance;
    private float clampedInterval = 1080f;
    [SerializeField] BackgroundFade fade;

    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.delta.y;
        float yPos = scrollRect.content.anchoredPosition.y;
        yPos += deltaY;

        clampedDistance = (SongChoiceManager.Instance.songPanelList.Count - 1) * clampedInterval;
        yPos = Mathf.Clamp(yPos, 0, clampedDistance);
        Vector2 newPosition = new Vector2(0, yPos);

        if(scrollRect.content.anchoredPosition != newPosition)
        {
            SongChoiceManager.Instance.ChoiceSong(true);
            playButton.GetComponent<RectTransform>().DOScale(0, 0.2f);
            Invoke(nameof(ActiveButton), 0.2f);
        }
        scrollRect.content.anchoredPosition = newPosition;
        fade.BackgroundFadeEffect();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        float yPos = scrollRect.content.anchoredPosition.y;
        float roundedY = Mathf.Round(yPos / snapDistance) * snapDistance;
        Vector2 snappedPosition = new Vector2(0, roundedY);
        StartCoroutine(SmoothSnap(snappedPosition));
    }
    private void ActiveButton() => playButton.gameObject.SetActive(false);

    private IEnumerator SmoothSnap(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; 
        Vector2 startPosition = scrollRect.content.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            scrollRect.content.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            fade.BackgroundFadeEffect();
            yield return null;
        }

        scrollRect.content.anchoredPosition = targetPosition;
        SongChoiceManager.Instance.ChoiceSong(false);
        playButton.gameObject.SetActive(true);
        playButton.GetComponent<RectTransform>().DOScale(1, 0.2f);
    }

}