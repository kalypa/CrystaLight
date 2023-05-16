using Redcode.Pools;
using System;
using UnityEngine;

public class Lane : LaneSetting
{
    private void Start() => sprite = holdLaneImage.GetComponent<SpriteRenderer>();
    void Update() => LaneUpdate();

    void LaneUpdate()
    {
        judgementText.JudgementTextTimer();
        SetNote();
        NoteInput();
        if (InputException()) LongNoteMetronome(notes[inputIndex].ParticleIndex());
    }
    public void NotSOInit()
    {
        noteSO = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songNotes[laneNum];
        bpm = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.bpm;
    }
    void SetNote()
    {
        if (spawnIndex < noteSO.timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= noteSO.timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                if (LaneCheck(noteSO.sameTimeInSideLaneList, sideIndex))
                {
                    SpawnNote(Note.NoteType.Invisible, laneNum != 3 , laneNum + 1, 0, true);
                    sideIndex++;
                }
                else if (LaneCheck(noteSO.sameTimeInCrossLaneList, crossIndex))
                {
                    if (IsCross() == isCrossLane)
                    {
                        SpawnNote(Note.NoteType.Invisible, laneNum < 2, laneNum + 2, laneNum - 2, true);
                        crossIndex++;
                    }
                    else
                    {
                        SpawnNote(Note.NoteType.Penetration, laneNum < 2, laneNum + 2, laneNum - 2, false);
                        crossIndex++;
                    }
                }
                else if (LaneCheck(noteSO.sameTimeInRefractionLaneList, refractionIndex))
                {
                    SpawnNote(Note.NoteType.Refraction, laneNum != 3, laneNum + 1, 0, false);
                    refractionIndex++;
                }
                else NoteLengthCheck();
            }
        }
    }

    void DestroyNote()
    {
        PoolManager.Instance.TakeToPool<Note>("Lane" + laneNum.ToString(), notes[inputIndex]);
        notes[inputIndex] = null;
        inputIndex++;
    }
    void NoteInput()
    {
        if (inputIndex < noteSO.timeStamps.Count)
        {
            double timeStamp = noteSO.timeStamps[inputIndex];
            double endTimeStamp = noteSO.endTimeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double perfectTime = 0.05f;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input)) KeyDownAction(audioTime, timeStamp, marginOfError);
            if (notes.Count - 1 >= inputIndex && notes[inputIndex].isHolding)
            {
                if (Input.GetKey(input)) KeyHoldAction(audioTime, endTimeStamp, perfectTime);
                if (Input.GetKeyUp(input)) KeyUpAction(audioTime, endTimeStamp, marginOfError);
            }
            if (timeStamp + marginOfError <= audioTime) MissAction();

            HoldLaneActiveFalse();
        }
        HoldLaneActiveFalse();
    }

    void KeyDownAction(double audioTime, double timeStamp, double marginOfError)
    {
        HoldInit();
        if (Math.Abs(audioTime - timeStamp) < marginOfError)
        {
            judgementText.TimerInit();
            judgementText.AccuracyJudgement(audioTime, timeStamp);
            Hit(notes[inputIndex].ParticleIndex());
            notes[inputIndex].isHolding = false;
            if (notes[inputIndex].type != Note.NoteType.Long)
            {
                HoldLaneEffect(notes[inputIndex].ParticleIndex());
                DestroyNote();
            }
            else
            {
                HoldInit();
                notes[inputIndex].isHolding = true;
                notes[inputIndex].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        else if (audioTime - timeStamp >= marginOfError) Miss();
    }

    void KeyHoldAction(double audioTime, double endTimeStamp, double perfectTime)
    {
        if (Math.Abs(audioTime - endTimeStamp) < perfectTime)
        {
            Hit(notes[inputIndex].ParticleIndex());
            notes[inputIndex].isHolding = false;
            judgementText.LongNoteAccuracyJudgement();
            DestroyNote();
        }
    }

    void KeyUpAction(double audioTime, double endTimeStamp, double marginOfError)
    {
        if (endTimeStamp - audioTime < marginOfError)
        {
            Hit(notes[inputIndex].ParticleIndex());
            notes[inputIndex].isHolding = false;
            judgementText.AccuracyJudgement(audioTime, endTimeStamp);
            DestroyNote();
        }
        else if (endTimeStamp - audioTime >= marginOfError)
        {
            notes[inputIndex].isHolding = false;
            Miss();
        }
    }
    void MissAction()
    {
        if (InputException())
        {
            if (notes[inputIndex].isMissed)
            {
                if (notes[inputIndex].type != Note.NoteType.Long) Miss();
                else
                {
                    notes[inputIndex].isHolding = false;
                    if (!notes[inputIndex].isHolding) Miss();
                }
            }
        }
    }
}