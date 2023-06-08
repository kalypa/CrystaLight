using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public GameObject endUI;
    public GameObject rankText;
    public GameObject fullComboText;
    public GameObject comboText;
    public GameObject comboNumText;
    public GameObject judgementText;
    public LaneAgent[] laneAI;
    private void Start() => fullComboText.transform.localScale = Vector3.zero;
    private void Update() => End();
    public void End()
    {
        var audio = SongManager.Instance.audioSource;
        rankText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -250);
        if (audio.time >= audio.clip.length && !IsFullCombo())
        {
            if(!GameManager.Instance.isAuto)
            {
                Invoke(nameof(EndUIActive), 1.5f);
            }
            else
            {
                foreach (LaneAgent ai in laneAI) ai.EndEpisode();
                SceneManager.LoadScene("MLAgentPlayScene");
            }
        }
        else if(audio.time >= audio.clip.length && IsFullCombo())
        {
            if(!GameManager.Instance.isAuto)
            {
                TextActiveFalse();
                fullComboText.transform.DOScale(1, 0.3f);
                Invoke(nameof(EndUIActive), 1.5f);
            }
            else
            {
                foreach (LaneAgent ai in laneAI) ai.EndEpisode();
                SceneManager.LoadScene("MLAgentPlayScene");
            }
        }
    }
    private bool IsFullCombo() => ScoreManager.Instance.missCount == 0 && SongManager.Instance.audioSource.time >= SongManager.Instance.audioSource.clip.length;

    void EndUIActive()
    {
        endUI.SetActive(true);
        rankText.GetComponent<RectTransform>().DOAnchorPos3DZ(0, 0.1f);
        EndUI.Instance.EndTexts();
    }

    void TextActiveFalse()
    {
        comboText.SetActive(false);
        comboNumText.SetActive(false);
        judgementText.SetActive(false);
    }
}
