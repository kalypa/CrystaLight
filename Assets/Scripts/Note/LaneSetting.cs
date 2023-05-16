using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Redcode.Pools;
using System;

public class LaneSetting : MonoBehaviour
{
    public List<Note> notes = new();
    public int laneNum;
    [HideInInspector] public NotesSO noteSO;
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    public GameObject longNotePrefab;
    public GameObject[] hitParticles = new GameObject[4];
    public JudgementText judgementText;
    public GameObject holdLaneImage;
    public Material[] backgroundMaterial = new Material[4];
    public Lane crossLane;
    public Lane sideLane;
    public Lane refractionLane;
    public DiamondEffect diamond;
    public int spawnIndex = 0;
    public int inputIndex = 0;
    public int sideIndex = 0;
    public int refractionIndex = 0;
    public int crossIndex = 0;
    public bool isCrossLane;
    protected float bpm;
    private double nextTick = 0.0;
    private bool isLongNoteStart = false;
    private float timer = 0;
    protected SpriteRenderer sprite;
    public Sprite[] holdImages;
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array, List<double> timeStamp, bool isLongNote)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                long time;
                if (isLongNote) time = note.EndTime;
                else time = note.Time;
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(time, SongManager.midiFile.GetTempoMap());
                timeStamp.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                noteSO.noteLengthList.Add(note.Length);
            }
        }
    }
    public void FindSameTimeInOtherLaneTimeStamp(Lane lane, List<double> list)
    {
        foreach (var t in lane.noteSO.timeStamps) if (noteSO.timeStamps.Contains(t) == true) list.Add(t);
    }

    protected bool IsCross()
    {
        if (crossIndex % 2 == 0) return true;
        return false;
    }
    protected void ParticleDisable(int idx, GameObject[] particles) => particles[idx].SetActive(false);
    protected void HoldLaneEffect(int idx)
    {
        HoldInit();
        sprite.sprite = holdImages[idx];
        holdLaneImage.transform.DOScaleX(0, 0.4f);
    }

    protected void HoldInit()
    {
        isLongNoteStart = false;
        var originScale = 1;
        holdLaneImage.transform.localScale = new Vector2(originScale, holdLaneImage.transform.localScale.y);
        holdLaneImage.SetActive(true);
    }
    void ParticleTimerInit() => timer = 0;
    void ParticleTimer(int idx, GameObject[] particle)
    {
        if (particle[idx].activeSelf == true)
        {
            ParticleDisable(idx, hitParticles);
            hitParticles[idx].SetActive(true);
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            particle[idx].SetActive(true);
        }

        if (timer >= 2) ParticleDisable(idx, hitParticles);
    }
    protected void Hit(int idx)
    {
        judgementText.TimerInit();
        ParticleTimerInit();
        diamond.ParticleTimerInit();
        ParticleTimer(idx, hitParticles);
        diamond.ParticleTimer(idx);
        ScoreManager.Instance.Hit();
        notes[inputIndex].isMissed = false;
    }
    protected void HoldLaneActiveFalse()
    {
        if (Input.GetKeyUp(input)) holdLaneImage.SetActive(false);
    }

    protected void Miss()
    {
        judgementText.TimerInit();
        judgementText.ViewMissText();
        ScoreManager.Instance.Miss();
        notes[inputIndex] = null;
        inputIndex++;
    }

    protected void NoteLengthCheck()
    {
        if (noteSO.noteLengthList[spawnIndex] == 32) SpawnNote(Note.NoteType.Normal, false, 0, 0, false);
        else SpawnNote(Note.NoteType.Long, false, 0, 0, false);
    }

    protected bool LaneCheck(List<double> laneList, int laneIndex) => laneList.Count - 1 >= laneIndex && laneList[laneIndex] == noteSO.timeStamps[spawnIndex];
    GameObject NoteLinkedCheck(int idx) => laneNum switch
    {
        0 => notes[spawnIndex].noteLinks1[idx],
        1 => notes[spawnIndex].noteLinks2[idx],
        2 => notes[spawnIndex].noteLinks3[idx],
        3 => notes[spawnIndex].noteLinks4[idx],
        _ => throw new NotImplementedException(),
    };
    protected void Link(bool numCk, int num1, int num2)
    {
        if (numCk) NoteLinkedCheck(num1).SetActive(true);
        else NoteLinkedCheck(num2).SetActive(true);
    }
    protected void SpawnNote(Note.NoteType n, bool laneCk, int num1, int num2, bool isLinked)
    {
        var note = PoolManager.Instance.GetFromPool<Note>(laneNum);
        note.type = n;
        notes.Add(note);
        note.endTime = noteSO.endTimeStamps[spawnIndex];
        if(isLinked) Link(laneCk, num1, num2);
        spawnIndex++;
    }

    protected void LongNoteMetronome(int idx)
    {
        if (InputException() && notes[inputIndex].isHolding)
        {
            if (!isLongNoteStart)
            {
                nextTick = AudioSettings.dspTime + 60.0 / (bpm * 8);
                isLongNoteStart = true;
            }
            double currentTime = AudioSettings.dspTime;
            if (currentTime >= nextTick)
            {
                judgementText.LongNoteAccuracyJudgement();
                Hit(idx);
                nextTick += 60.0 / (bpm * 8);
            }
        }
    }

    protected bool InputException() => notes.Count - 1 >= inputIndex && notes[inputIndex] != null;
}
