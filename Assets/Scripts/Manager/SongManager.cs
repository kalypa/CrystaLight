using System.Collections;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System.Numerics;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError;    // ��Ʈ�� ������ ���� �÷��̾��� �Է� ����

    public int inputDelayInMilliseconds;   // �Է� ���� �ð� (�и��� ����)

    private string fileLocation; // �̵� ������ ��ġ
    public float noteTime;  // ��Ʈ�� �����Ǵ� �ð�
    public float longNoteTime;  // ��Ʈ�� �����Ǵ� �ð�
    public float noteSpawnY;    // ��Ʈ�� �����Ǵ� ��ġ (������)
    public float noteTapY;  // ��Ʈ�� ���ϴ� ��ġ (������)
    public float noteDespawnY    // ��Ʈ�� ������� ��ġ (������)
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static MidiFile midiFile;    // �ҷ��� �̵� ����

    void Start()
    {
        Instance = this;
        inputDelayInMilliseconds = GameManager.Instance.offset; // GameManager���� ������ �Է� ���� �ð�
        ReadFromFile();
    }

    private void ReadFromFile() // ���ÿ��� �̵� ������ �о���̴� �޼ҵ�
    {
        fileLocation = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songName.ToString() + ".mid";
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);  // �̵� ������ �Ľ��Ͽ� ������ ����
        GetDataFromMidi();  // �̵� ���Ͽ��� �����͸� �����Ͽ� ���ο� �Ҵ��ϴ� �޼ҵ� ����
    }

    public void GetDataFromMidi()   // �̵� ���Ͽ��� �����͸� �����Ͽ� ���ο� �Ҵ��ϴ� �޼ҵ�
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);    // �迭�� �̵� ���Ͽ��� ������ ��Ʈ �̺�Ʈ ������ ����

        Invoke(nameof(StartSong), songDelayInSeconds);  // ������ �ð� �� ���� �����ϴ� �޼ҵ带 ȣ���ϴ� Invoke �Լ�
        foreach (var lane in lanes) lane.NotSOInit();
        //foreach (var lane in lanes) lane.SetTimeStamps(array);  // �� ���ο� ��Ʈ �̺�Ʈ ������ �Ҵ��ϴ� �޼ҵ� ȣ��
        //foreach (var lane in lanes) lane.SetEndTimeStamps(array);  // �� ���ο� ��Ʈ �̺�Ʈ ������ �Ҵ��ϴ� �޼ҵ� ȣ��
        foreach (var lane in lanes)
        {
            lane.FindSameTimeInOtherLaneTimeStamp(lane.sideLane, lane.sideLane.noteSO.sameTimeInSideLaneList);
            lane.FindSameTimeInOtherLaneTimeStamp(lane.crossLane, lane.crossLane.noteSO.sameTimeInCrossLaneList);
            lane.FindSameTimeInOtherLaneTimeStamp(lane.refractionLane, lane.refractionLane.noteSO.sameTimeInRefractionLaneList);
        }
    }

    public void StartSong() // �� ����
    {
        audioSource.clip = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songAudioSource.clip;
        audioSource.Play(); // AudioSource ������Ʈ�� Play �޼ҵ� ȣ��
    }

    public static double GetAudioSourceTime() // ���� ���� ����ǰ� �ִ� �ð��� ��ȯ�ϴ� �޼ҵ�
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency; //���� ����� �ҽ������� ��� �ð��� �� ������ ����ϰ�, �̸� ��ȯ
    }
}