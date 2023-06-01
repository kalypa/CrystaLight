using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    public static EndUI Instance;

    private void Awake() => Instance = this;

    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accuracyText;
    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private TMP_Text greatText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text missText;
    private int clearCount = 0;
    public void EndTexts()
    {
        var accuracy = ScoreManager.Instance.CalculateHitAccuracy();
        rankText.text = accuracy switch
        {
            >= 95 => "S",
            >= 90 and < 95 => "A",
            >= 80 and < 90 => "B",
            >= 70 and < 80 => "C",
            _ => "F"
        };
        TextChange();
        SaveData();
    }

    void TextChange()
    {
        if(rankText.text != "F") clearCount++;
        var instance = ScoreManager.Instance;
        scoreText.text = instance.scoreText.text;
        accuracyText.text = instance.hitAccuracyText.text;
        perfectText.text = instance.perfectCount.ToString();
        greatText.text = instance.greatCount.ToString();
        goodText.text = instance.goodCount.ToString();
        missText.text = instance.missCount.ToString();
    }

    void SaveData()
    {
        if (rankText.text != "F")
        {
            var instance = ScoreManager.Instance;
            var gameInstance = GameManager.Instance;
            if (gameInstance.recordSO.scoreText[StageManager.Instance.currentStageNum] < instance.score) gameInstance.recordSO.scoreText[StageManager.Instance.currentStageNum] = instance.score;
            if (gameInstance.recordSO.accuracyText[StageManager.Instance.currentStageNum] < instance.CalculateHitAccuracy()) gameInstance.recordSO.accuracyText[StageManager.Instance.currentStageNum] = instance.CalculateHitAccuracy();
            if (gameInstance.recordSO.maxComboText[StageManager.Instance.currentStageNum] < instance.maxCombo) gameInstance.recordSO.maxComboText[StageManager.Instance.currentStageNum] = instance.maxCombo;
            if (gameInstance.recordSO.clearText[StageManager.Instance.currentStageNum] < clearCount) gameInstance.recordSO.clearText[StageManager.Instance.currentStageNum] = clearCount;
            gameInstance.recordSO.ratingText[StageManager.Instance.currentStageNum] = gameInstance.recordSO.accuracyText[StageManager.Instance.currentStageNum] switch
            {
                >= 95 => "S",
                >= 90 and < 95 => "A",
                >= 80 and < 90 => "B",
                >= 70 and < 80 => "C",
                _ => "F"
            };
            DataManager.Instance.JsonSave();
        }
    }
}
