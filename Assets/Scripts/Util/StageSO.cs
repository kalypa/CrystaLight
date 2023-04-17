using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Stage/StageSO")]
public class StageSO : ScriptableObject
{
    public SongSO stageSong;
    public int stageNum;
    public int stageDifficulty;
    public AudioClip previewSong;
}
