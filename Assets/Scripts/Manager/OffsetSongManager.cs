using Melanchall.DryWetMidi.Core;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;

public class OffsetSongManager : MonoBehaviour
{
    public static OffsetSongManager Instance;
    public AudioSource audioSource;
    public double marginOfError;    

    public int inputDelayInMilliseconds;   
    public OffsetSceneLane lane;
    private string fileLocation; 

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
        lane.SetTimeStamps(array);  
    }

    public void StartSong() => audioSource.Play();

    public static double GetAudioSourceTime()  => (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
}
