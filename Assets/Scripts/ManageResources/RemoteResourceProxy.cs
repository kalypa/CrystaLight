using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteResourceProxy : IResource
{
    public NotesSO defaultNotes;
    private LocalResource _localResource;

    public RemoteResourceProxy()
    {
        _localResource = new LocalResource();
    }

    private void ClearResourceData()
    {
        defaultNotes.timeStamps.Clear();
        defaultNotes.endTimeStamps.Clear();
        defaultNotes.sameTimeInSideLaneList.Clear();
        defaultNotes.sameTimeInRefractionLaneList.Clear();
        defaultNotes.sameTimeInCrossLaneList.Clear();
        defaultNotes.noteLengthList.Clear();
    }

    public void Load()
    {
        defaultNotes = Resources.Load("ScriptableObjects/Collections/PlayingSongNotes") as NotesSO;
        ClearResourceData();
        _localResource.ChangingDataFromResource(defaultNotes);
    }
}
