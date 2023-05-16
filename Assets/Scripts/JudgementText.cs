using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class JudgementText : MonoBehaviour
{
    public List<Sprite> accuracySprites;
    public ComboText comboNumText;
    public ComboText comboText;
    private float timer = 0;                                               
    private SpriteRenderer spriteRenderer;
    private TMP_Text comboNum;
    private TMP_Text combo;
    private TouchEffect effect;
    public void Start() => Init();

    void Init()
    {
        DataManager.Instance.JudgementTextDataInit(accuracySprites);
        spriteRenderer = GetComponent<SpriteRenderer>();
        comboNum = comboNumText.GetComponent<TMP_Text>();
        combo = comboText.GetComponent<TMP_Text>();
        effect = GetComponent<TouchEffect>();
    }
    public void TimerInit() => timer = 0;

    public void AccuracyJudgement(double audioT, double timeStamp)
    {
        if(Math.Abs(audioT - timeStamp) <= 0.05)
        {
            ScoreManager.Instance.perfectCount++;
            JudgementCount(ScoreManager.Instance.perfectScore, accuracySprites[0]);
        }
        else if (Math.Abs(audioT - timeStamp) > 0.05)
        {
            if (Math.Abs(audioT - timeStamp) <= 0.1f)
            {
                ScoreManager.Instance.greatCount++;
                JudgementCount(ScoreManager.Instance.greatScore, accuracySprites[1]);
            }

            else if (Math.Abs(audioT - timeStamp) > 0.09 )
            {
                if (Math.Abs(audioT - timeStamp) <= 0.15f)
                {
                    ScoreManager.Instance.goodCount++;
                    JudgementCount(ScoreManager.Instance.goodScore, accuracySprites[2]);
                }
            }
        }
    }

    void JudgementCount(int score, Sprite sprite)
    {
        ScoreManager.Instance.CalculateScore(score);
        JudgementTextChange(sprite);
        JudgementEffect();
    }
    public void LongNoteAccuracyJudgement()
    {
        ScoreManager.Instance.perfectCount++;
        JudgementCount(ScoreManager.Instance.perfectScore, accuracySprites[0]);
    }
    public void JudgementTextTimer()
    {
        if (spriteRenderer.enabled == true) timer += Time.deltaTime;
        else timer = 0;

        if (timer >= 2 && spriteRenderer.sprite == accuracySprites[3]) spriteRenderer.enabled = false; 
    }

    public void JudgementTextChange(Sprite s)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = s;
        comboNum.enabled = true;
        combo.enabled = true;
    }

    public void JudgementEffect()
    {
        comboNumText.ComboTextEffect();
        comboText.ComboTextEffect();
        effect.ComboTextEffect();
    }

    public void ViewMissText()
    {
        spriteRenderer.enabled = true;
        comboNum.enabled = false;
        combo.enabled = false;
        spriteRenderer.sprite = accuracySprites[3];
        effect.ComboTextEffect();
        JudgementEffect();
    }
}
