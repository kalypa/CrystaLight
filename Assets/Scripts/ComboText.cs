using DG.Tweening;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    [SerializeField]
    private float scaleOffset = 0.25f;
    private RectTransform rectTransform;

    private void Start() => rectTransform = GetComponent<RectTransform>();
    public void ComboTextEffect()
    {
        DOTween.KillAll();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 OriginalScale = rectTransform.localScale;
        DOTween.Sequence().Append(rectTransform.DOScale(new Vector3(OriginalScale.x + scaleOffset, OriginalScale.y + scaleOffset, OriginalScale.z + scaleOffset), 0.02f).SetEase(Ease.Linear)).Append(rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0.02f).SetEase(Ease.Linear));
    }
}
