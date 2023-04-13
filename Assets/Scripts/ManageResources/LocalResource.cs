using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalResource : IResource
{
    private NotesSO localNotes;

    public void ChangingDataFromResource(NotesSO resource)
    {
        resource.timeStamps.AddRange(localNotes.timeStamps);
        resource.endTimeStamps.AddRange(localNotes.endTimeStamps);
        resource.sameTimeInSideLaneList.AddRange(localNotes.sameTimeInSideLaneList);
        resource.sameTimeInRefractionLaneList.AddRange(localNotes.sameTimeInRefractionLaneList);
        resource.sameTimeInCrossLaneList.AddRange(localNotes.sameTimeInCrossLaneList);
        resource.noteLengthList.AddRange(localNotes.noteLengthList);
    }

    public void Load()
    {
        localNotes = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songNotes;
    }
}

