using UnityEngine;
using TMPro;

public class AudioLengthDisplay : MonoBehaviour
{
    private AudioClip audioClip;
    [SerializeField] private TextMeshProUGUI audioLengthText;

    private void Update()
    {
        UpdateAudioLengthText();
    }

    private void UpdateAudioLengthText()
    {
        audioClip = StageManager.Instance.stageList[StageManager.Instance.currentStageNum].stageSong.songAudioSource.clip;
        if (audioClip == null || audioLengthText == null) return;

        float audioLengthInSeconds = audioClip.length;
        int minutes = Mathf.FloorToInt(audioLengthInSeconds / 60f);
        int seconds = Mathf.FloorToInt(audioLengthInSeconds % 60f);

        audioLengthText.text = string.Format("{0} : {1:D2}", minutes, seconds);
    }
}
