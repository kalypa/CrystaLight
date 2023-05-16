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
    }

    void TextChange()
    {
        var instance = ScoreManager.Instance;
        scoreText.text = instance.scoreText.text;
        accuracyText.text = instance.hitAccuracyText.text;
        perfectText.text = ": " + instance.perfectCount.ToString();
        greatText.text = ": " + instance.greatCount.ToString();
        goodText.text = ": " + instance.goodCount.ToString();
        missText.text = ": " + instance.missCount.ToString();
    }
}
