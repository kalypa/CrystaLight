using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneSongVolume : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    void Update()
    {
        music.volume = GameManager.Instance.volume;
    }
}
