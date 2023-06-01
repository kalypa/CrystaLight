using TMPro;
using UnityEngine;

public class RecordInit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI rating;
    [SerializeField] private TextMeshProUGUI maxCombo;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI clear;

    void Update()
    {
        score.text = GameManager.Instance.recordSO.scoreText[StageManager.Instance.currentStageNum].ToString();
        rating.text = GameManager.Instance.recordSO.ratingText[StageManager.Instance.currentStageNum];
        maxCombo.text = GameManager.Instance.recordSO.maxComboText[StageManager.Instance.currentStageNum].ToString();
        accuracy.text = GameManager.Instance.recordSO.accuracyText[StageManager.Instance.currentStageNum].ToString("F2") + "%";
        clear.text = GameManager.Instance.recordSO.clearText[StageManager.Instance.currentStageNum].ToString();
    }
}
