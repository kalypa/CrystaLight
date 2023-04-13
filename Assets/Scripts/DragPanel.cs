using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour, IDragHandler
{
    public float snapDistance = 1080f;
    public ScrollRect scrollRect;
    private float clampedDistance;
    private float clampedInterval = 1080f;
    public void OnDrag(PointerEventData eventData)
    {
        clampedDistance = (SongChoiceManager.Instance.songPanelList.Count - 1) * clampedInterval;
        float yPos = scrollRect.content.anchoredPosition.y;
        if(yPos >= clampedDistance)
        {
            yPos = clampedDistance;
        }
        float roundedY = Mathf.Round(yPos / snapDistance) * snapDistance;
        Vector2 newPosition = new Vector2(0, roundedY);
        scrollRect.content.anchoredPosition = newPosition;
        
    }

    
}