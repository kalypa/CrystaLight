using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    public static EndUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accuracyText;
    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private TMP_Text greatText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text missText;

    public void EndTexts()
    {
        if(ScoreManager.Instance.CalculateHitAccuracy() >= 95)
        {
            rankText.text = "S";
        }
        else if(ScoreManager.Instance.CalculateHitAccuracy() >= 90 && ScoreManager.Instance.CalculateHitAccuracy() < 95)
        {
            rankText.text = "A";
        }
        else if(ScoreManager.Instance.CalculateHitAccuracy() >= 80 && ScoreManager.Instance.CalculateHitAccuracy() < 90)
        {
            rankText.text = "B";
        }
        else if(ScoreManager.Instance.CalculateHitAccuracy() >= 70 && ScoreManager.Instance.CalculateHitAccuracy() < 80)
        {
            rankText.text = "C";
        }
        else if(ScoreManager.Instance.CalculateHitAccuracy() < 70)
        {
            rankText.text = "F";
        }
        scoreText.text = ScoreManager.Instance.scoreText.text;
        accuracyText.text =  ScoreManager.Instance.hitAccuracyText.text + "%";
        perfectText.text = ": " + ScoreManager.Instance.perfectCount.ToString();
        greatText.text = ": " +  ScoreManager.Instance.greatCount.ToString();
        goodText.text = ": " + ScoreManager.Instance.goodCount.ToString();
        missText.text = ": " + ScoreManager.Instance.missCount.ToString();
    }
}
