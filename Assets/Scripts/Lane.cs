using DG.Tweening;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    public GameObject longNotePrefab;
    public GameObject hitParticle;
    public JudgementText judgementText;
    public GameObject holdLaneImage;
    public Lane crossLane;
    public Lane sideLane;
    public Lane refractionLane;
    public List<Note> notes = new();
    public int spawnIndex = 0;
    public int inputIndex = 0;
    public int sideIndex = 0;
    public int refractionIndex = 0;
    public int crossIndex = 0;
    public bool isCrossLane;
    [HideInInspector] public NotesSO noteSO;
    public int laneNum;
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
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
    }

    public void SetEndTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        {
            foreach (var note in array)
            {
                if (note.NoteName == noteRestriction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.EndTime, SongManager.midiFile.GetTempoMap());
                    noteSO.endTimeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                    noteSO.noteLengthList.Add(note.Length);
                }
            }
        }
    }
    public void NotSOInit()
    {
        noteSO = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songNotes[laneNum];
    }
    void Update()
    {
        judgementText.JudgementTextTimer();
        SetNote();
        NoteInput();
    }

    void SetNote()
    {
        if (spawnIndex < noteSO.timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= noteSO.timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                if (noteSO.sameTimeInSideLaneList.Count - 1 >= sideIndex && noteSO.sameTimeInSideLaneList[sideIndex] == noteSO.timeStamps[spawnIndex])
                {
                    SpawnNote(Note.NoteType.Invisible);
                    sideIndex++;
                }
                else if (noteSO.sameTimeInCrossLaneList.Count - 1 >= crossIndex && noteSO.sameTimeInCrossLaneList[crossIndex] == noteSO.timeStamps[spawnIndex])
                {
                    if (isCross() == isCrossLane)
                    {
                        SpawnNote(Note.NoteType.Invisible);
                        crossIndex++;
                    }
                    else
                    {
                        SpawnNote(Note.NoteType.Penetration);
                        crossIndex++;
                    }
                }
                else if (noteSO.sameTimeInRefractionLaneList.Count - 1 >= refractionIndex && noteSO.sameTimeInRefractionLaneList[refractionIndex] == noteSO.timeStamps[spawnIndex])
                {
                    SpawnNote(Note.NoteType.Refraction);
                    refractionIndex++;
                }
                else
                {
                    if (noteSO.noteLengthList[spawnIndex] == 32)
                    {
                        SpawnNote(Note.NoteType.Normal);
                    }
                    else
                    {
                        SpawnNote(Note.NoteType.Long);
                    }
                }
            }
        }
    }

    void SpawnNote(Note.NoteType n)
    {
        var note = Instantiate(notePrefab, transform);
        note.GetComponent<Note>().type = n;
        notes.Add(note.GetComponent<Note>());
        note.GetComponent<Note>().endTime = noteSO.endTimeStamps[spawnIndex];
        spawnIndex++;
    }

    void NoteInput()
    {
        if (inputIndex < noteSO.timeStamps.Count)
        {
            double timeStamp = noteSO.timeStamps[inputIndex];
            double endTimeStamp = noteSO.endTimeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                HoldInit();
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    judgementText.TimerInit();
                    judgementText.AccuracyJudgement(audioTime, timeStamp);
                    Hit();
                    if (notes[inputIndex].type != Note.NoteType.Long)
                    {
                        HoldLaneEffect();
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                    else
                    {
                        HoldInit();
                        notes[inputIndex].isHolding = true;
                        notes[inputIndex].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    OffsetCheck(audioTime, timeStamp);
                }

                else if (audioTime - timeStamp >= marginOfError)
                {
                    judgementText.TimerInit();
                    judgementText.ViewMissText();
                    Miss();
                    inputIndex++;
                    OffsetCheck(audioTime, timeStamp);
                }
            }
            if (notes.Count - 1 >= inputIndex && notes[inputIndex].isHolding)
            {
                if (Input.GetKey(input))
                {
                    if (Math.Abs(audioTime - endTimeStamp) < marginOfError)
                    {
                        judgementText.TimerInit();
                        judgementText.LongNoteAccuracyJudgement();
                        Hit();
                        notes[inputIndex].isHolding = false;
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                }
                if (Input.GetKeyUp(input))
                {
                    if (audioTime - endTimeStamp >= marginOfError)
                    {
                        judgementText.TimerInit();
                        judgementText.ViewMissText();
                        Miss();
                        notes[inputIndex].isHolding = false;
                        inputIndex++;
                    }
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                if (notes.Count - 1 >= inputIndex && notes[inputIndex] != null)
                {
                    if (notes[inputIndex].type != Note.NoteType.Long)
                    {
                        judgementText.TimerInit();
                        judgementText.ViewMissText();
                        Miss();
                        inputIndex++;
                    }
                    else
                    {
                        if (!notes[inputIndex].isHolding)
                        {
                            judgementText.TimerInit();
                            judgementText.ViewMissText();
                            Miss();
                            inputIndex++;
                        }
                    }
                }
            }

            if (Input.GetKeyUp(input))
            {
                holdLaneImage.SetActive(false);
            }
        }
        if (Input.GetKeyUp(input))
        {
            holdLaneImage.SetActive(false);
        }
    }

    public void FindSameTimeInOtherLaneTimeStamp(Lane lane, List<double> list)
    {
        foreach (var t in lane.noteSO.timeStamps)
        {
            if (noteSO.timeStamps.Contains(t) == true)
            {
                list.Add(t);
            }
        }
    }

    void OffsetCheck(double audioTime, double timeStamp)
    {
        if (FindObjectOfType<OffsetManager>() != null)
            OffsetMeasure(Math.Abs(audioTime - timeStamp));
    }

    bool isCross()
    {
        if (crossIndex % 2 == 0)
        {
            return true;
        }
        return false;
    }

    private void Hit()
    {
        if (FindObjectOfType<ScoreManager>() != null)        //현재 Scene에 ScoreManager가 있을 때만 실행
        {
            if (hitParticle.activeSelf == false)
                hitParticle.SetActive(true);
            else
            {
                hitParticle.SetActive(false);
                hitParticle.SetActive(true);
            }
            Invoke("ParticleDisable", 0.2f);
            ScoreManager.Instance.Hit();
        }
    }

    private void Miss()
    {
        if (FindObjectOfType<ScoreManager>() != null)        //현재 Scene에 ScoreManager가 있을 때만 실행
            ScoreManager.Instance.Miss();
    }

    private void ParticleDisable()
    {
        hitParticle.SetActive(false);
    }

    private void OffsetMeasure(double d)
    {
        if (FindObjectOfType<OffsetManager>() == null)
            return;
        OffsetManager.Instance.offsetList.Add(d);
    }

    void HoldLaneEffect()
    {
        var originScale = 1;
        holdLaneImage.transform.localScale = new Vector2(originScale, holdLaneImage.transform.localScale.y);
        holdLaneImage.SetActive(true);
        holdLaneImage.transform.DOScaleX(0, 0.4f);
    }

    void HoldInit()
    {
        var originScale = 1;
        holdLaneImage.transform.localScale = new Vector2(originScale, holdLaneImage.transform.localScale.y);
        holdLaneImage.SetActive(true);
    }
}