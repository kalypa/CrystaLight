using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : SingleMonobehaviour<ScoreManager>
{
    public TMP_Text scoreText;
    public TMP_Text comboNumText;
    public TMP_Text comboText;
    public TMP_Text hitAccuracyText;

    public int score = 0;
    public int perfectScore = 170;
    public int greatScore = 140;
    public int goodScore = 40;
    int combo = 0;
    public int maxCombo = 0;
    public int perfectCount = 0;
    public int greatCount = 0;
    public int goodCount = 0;
    int hitCount = 0;
    public int missCount = 0;
    private int currentSongNoteTotal = 0;
    void Start() => Init();
    void Init()
    {
        UpdateUI();
        foreach (var note in StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songNotes) currentSongNoteTotal += note.timeStamps.Count;
    }
    public void Hit()
    {
        hitCount++;
        combo++;
        score += 100 * combo;
        if (combo > maxCombo) maxCombo = combo;
        UpdateUI();
    }
    public void Miss()
    {
        missCount++;
        combo = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        comboText.text = "Combo";
        scoreText.text = score.ToString();
        comboNumText.text = combo.ToString();
        hitAccuracyText.text = CalculateHitAccuracy().ToString("F2") + "%";
    }

    public float CalculateHitAccuracy()
    {
        if (hitCount + missCount == 0) return 0;

        return (float)(((perfectCount * 100f) + (greatCount * 75f) + (goodCount * 40f)) / ( hitCount + missCount));
    }
    public void CalculateScore(int currentScore)
    {
        score += currentScore * (int)(1 + 1 / 3 * Mathf.Log10(Mathf.Min(Mathf.Max(combo, 1), currentSongNoteTotal)));
    }
    public void Reset()
    {
        score = 0;
        combo = 0;
        maxCombo = 0;
        hitCount = 0;
        missCount = 0;
        UpdateUI();
    }
}
