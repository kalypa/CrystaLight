using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour
{
    public AudioClip metronomeSound;
    public float bpm;

    private double nextTick = 0.0;

    void Start() => StartCoroutine(PlayMetronome());

    IEnumerator PlayMetronome()
    {
        yield return new WaitForSeconds(1f);
        nextTick = AudioSettings.dspTime + 60.0 / (bpm * 32);
        while (true)
        {
            double currentTime = AudioSettings.dspTime;
            if (currentTime >= nextTick)
            {
                Debug.Log("Tick");
                AudioSource.PlayClipAtPoint(metronomeSound, transform.position);
                nextTick += 60.0 / (bpm * 32);
            }
            yield return null;
        }
    }
}
