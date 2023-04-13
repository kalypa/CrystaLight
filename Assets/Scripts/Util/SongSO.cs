using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Song/SongSO")]
public class SongSO : ScriptableObject
{
    public NotesSO songNotes;
    public GameObject songAudioSource;
    public string songName;
    public string artistName;
    public Sprite background;
    public Sprite albumArt;
}
