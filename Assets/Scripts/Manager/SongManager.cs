using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;


public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError;   

    public int inputDelayInMilliseconds;  

    private string fileLocation; 
    public float noteTime; 
    public float longNoteTime;
    public float noteSpawnZ;  
    public float noteTapZ; 
    public float noteDespawnZ    
    {
        get
        {
            return noteTapZ - (noteSpawnZ - noteTapZ);
        }
    }

    public static MidiFile midiFile;
    private void Awake() => Instance = this;
    void Start() => Init();
    void Init()
    {
        inputDelayInMilliseconds = GameManager.Instance.offset;
        ReadFromFile();
    }

    private void ReadFromFile() 
    {
        fileLocation = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songName.ToString() + ".mid";
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation); 
        GetDataFromMidi();  
    }

    public void GetDataFromMidi()   
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);    

        Invoke(nameof(StartSong), songDelayInSeconds);  
        foreach (var lane in lanes) lane.NotSOInit();
        foreach (var lane in lanes) if(lane.noteSO.timeStamps.Count <= 0) lane.SetTimeStamps(array, lane.noteSO.timeStamps, false);  
        foreach (var lane in lanes) if (lane.noteSO.endTimeStamps.Count <= 0) lane.SetTimeStamps(array, lane.noteSO.endTimeStamps, true);  
        foreach (var lane in lanes)
        {
            if (lane.sideLane.noteSO.sameTimeInSideLaneList.Count <= 0) lane.FindSameTimeInOtherLaneTimeStamp(lane.sideLane, lane.sideLane.noteSO.sameTimeInSideLaneList);
            if (lane.crossLane.noteSO.sameTimeInCrossLaneList.Count <= 0) lane.FindSameTimeInOtherLaneTimeStamp(lane.crossLane, lane.crossLane.noteSO.sameTimeInCrossLaneList);
            if (lane.refractionLane.noteSO.sameTimeInRefractionLaneList.Count <= 0) lane.FindSameTimeInOtherLaneTimeStamp(lane.refractionLane, lane.refractionLane.noteSO.sameTimeInRefractionLaneList);
        }
    }

    public void StartSong() 
    {
        audioSource.clip = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songAudioSource.clip;
        audioSource.Play(); 
    }

    public static double GetAudioSourceTime() => (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
}