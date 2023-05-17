using System;
using UnityEngine;

public class SpawnSpectrum : MonoBehaviour
{
    public GameObject spectrumPrefab;
    GameObject[] spectrumLine = new GameObject[512];
    public float maxScale;

    void Start() => Init();

    void Update() => SetSpectrumScale();

    void Init()
    {
        SpawnSpectrumLine();
        SetScaleY();
    }
    void SpawnSpectrumLine()
    {
        for (int i = 0; i < spectrumLine.Length; i++)
        {
            GameObject spawnLine = Instantiate(spectrumPrefab);
            spawnLine.transform.position = transform.position;
            spawnLine.transform.parent = transform;
            transform.eulerAngles = new Vector3(0, 0, 0.703125f * i * 2);
            spawnLine.transform.position = Vector3.left * 5.55f;
            spectrumLine[i] = spawnLine;
            GradientRainbow(i);
        }
    }

    SpriteRenderer GetSpriteRenderer(int index) => spectrumLine[index].GetComponentInChildren<SpriteRenderer>();

    void GradientRainbow(int index)
    {
        float h = StageGradientH(index);
        float s = StageGradientS();
        float v = StageGradientV(index);
        GetSpriteRenderer(index).color = Color.HSVToRGB(h, s, v);
    }
    float StageGradientH(int index) => StageManager.Instance.currentStageNum switch
    {
        0 => 186 / 360f + CarculateGradient(202, 186) * index,
        1 => 44 / 360f + CarculateGradient(60, 44) * index,
        2 => 260 / 360f + CarculateGradient(276, 260) * index,
        3 => 234 / 360f + CarculateGradient(250, 234) * index,
        4 => 120 / 360f,
        5 => 344 / 360f + CarculateGradient(360, 344) * index,
        _ => throw new NotImplementedException(),
    };
    float StageGradientS() => StageManager.Instance.currentStageNum switch
    {
        0 => 1,
        1 => 22f / 100f,
        2 => 1,
        3 => 50f / 100f,
        4 => 0,
        5 => 1,
        _ => throw new NotImplementedException(),
    };
    float StageGradientV(int index) => StageManager.Instance.currentStageNum switch
    {
        0 => 1,
        1 => 75f / 100f,
        2 => 1,
        3 => 50f / 100f,
        4 => 80f / 100f - CarculateGradientV(80, 64) * index,
        5 => 1,
        _ => throw new NotImplementedException(),
    };
    float CarculateGradient(float a, float b) => ((a / 360) - (b / 360)) / 512;
    float CarculateGradientV(float a, float b) => ((a / 100) - (b / 100)) / 512;
    void SetSpectrumScale()
    {
        for (int i = 0; i < spectrumLine.Length; i++)
        {
            if (spectrumLine[i] != null) spectrumLine[i].transform.localScale = new Vector3(-(Equalizer.samples[i] * maxScale) - 0.111f, 0.0555f, 0.0555f);
            GradientRainbow(i);
        }
    }

    void SetScaleY() => transform.localScale = new Vector3(1, 0.97f, 1);
}
