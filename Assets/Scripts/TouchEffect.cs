using DG.Tweening;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    [SerializeField]
    private float scaleOffset = 0.5f;

    public void ComboTextEffect()
    {
        DOTween.KillAll();
        transform.localScale = new Vector3(1f, 1f, 18f);
        Vector3 OriginalScale = transform.localScale;
        DOTween.Sequence().Append(transform.DOScale(new Vector3(OriginalScale.x + scaleOffset, OriginalScale.y + scaleOffset, OriginalScale.z), 0.02f).SetEase(Ease.Linear)).Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.02f).SetEase(Ease.Linear));
    }
}
