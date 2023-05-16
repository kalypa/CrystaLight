using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Song/SongSO")]
public class SongSO : ScriptableObject
{
    public List<NotesSO> songNotes;
    public AudioSource songAudioSource;
    public string songName;
    public string artistName;
    public Sprite albumArt;
    public float bpm;
}
