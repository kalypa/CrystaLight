using Melanchall.DryWetMidi.Interaction;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OffsetSceneLane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public int inputIndex = 0;
    public NotesSO noteSO;
    public int laneNum;
    public Text offsetText;
    public Text startText;
    public GameObject hitParticle;
    private float timer;
    [SerializeField] private OffsetCheckStart offsetCheckStart;
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                noteSO.timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                noteSO.noteLengthList.Add(note.Length);
            }
        }
    }
    private void Start() => InitIndex();
    void Update() => NoteInput();

    void InitIndex() => inputIndex = 0;
    void NoteInput()
    {
        if (inputIndex < noteSO.timeStamps.Count)
        {
            double timeStamp = noteSO.timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    OffsetMeasure(audioTime - timeStamp);
                    inputIndex++;
                }

                else if (audioTime - timeStamp >= marginOfError) inputIndex++;   
            }
            if (timeStamp + marginOfError <= audioTime) inputIndex++;
        }
        else OffsetCheckEnd();
    }
    private void Hit()
    {
        ParticleTimerInit();
        ParticleTimer(hitParticle);
    }
    void ParticleTimerInit() => timer = 0;
    void ParticleTimer(GameObject particle)
    {
        if (particle.activeSelf == true)
        {
            ParticleDisable();
            hitParticle.SetActive(true);
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            particle.SetActive(true);
        }

        if (timer >= 2)
        {
            ParticleDisable();
        }
    }

    void ParticleDisable() => hitParticle.SetActive(false);
    private void OffsetMeasure(double d)
    {
        offsetText.text = (d * 1000).ToString() + "ms";
        OffsetManager.Instance.offsetList.Add(d);
    }
    void OffsetCheckEnd()
    {
        if(OffsetManager.Instance.offsetList != null) SetOffsetText("입력 딜레이: " + OffsetManager.Instance.GetOffset().ToString() + "ms");
        else SetOffsetText("다시 시도해보세요..");
    }

    void TextSetting(string text)
    {
        offsetText.text = text;
        startText.text = "아무 키나 눌러 다시 시작";
        offsetCheckStart.KeyDownCheck();
    }

    void SetOffsetText(string s)
    {
        TextSetting(s);
        InitIndex();
    }
}
