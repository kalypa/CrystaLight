using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalizer : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    void Start() => audioSource = GetComponent<AudioSource>();

    // Update is called once per frame
    void Update() => GetSpectrumAudioSource();

    void GetSpectrumAudioSource() => audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
}
