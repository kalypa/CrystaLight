using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerInit : MonoBehaviour
{
    void Start()
    {
        StageManager.Instance.songName = SongChoiceManager.Instance.songName;
        StageManager.Instance.artistName = SongChoiceManager.Instance.artistName;
    }
}
