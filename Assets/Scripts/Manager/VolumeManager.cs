using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;   
    [SerializeField] private AudioSource music;   
    [SerializeField] private GameObject volumeZero;   
    void Update()
    {
        if(Time.timeScale == 0) music.Pause();
        if (music.clip != null) music.volume = volumeSlider.value;
        GameManager.Instance.volume = volumeSlider.value;
        if (volumeSlider.value == 0) volumeZero.SetActive(true);
        else volumeZero.SetActive(false);

    }
}
