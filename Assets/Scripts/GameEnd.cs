using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject endUI;
    private void Update() => End();
    public void End()
    {
        var audio = SongManager.Instance.audioSource;
        if (audio.time >= audio.clip.length)
        {
            endUI.SetActive(true);
            EndUI.Instance.EndTexts();
        }
    }
}
