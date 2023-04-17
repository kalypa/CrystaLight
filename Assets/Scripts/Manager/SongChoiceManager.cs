using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongChoiceManager : MonoBehaviour
{
    public static SongChoiceManager Instance;

    public List<GameObject> songPanelList = new();
    [HideInInspector] public GameObject currentPanel = null;
    public ScrollRect scrollRect;
    private float panelDistance = 1080f;
    public AudioSource previewSong;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SongInfoInit();
        ChoiceSong();
    }

    void SongInfoInit()
    {
        for(int i = 0; i < songPanelList.Count; i++) 
        {
            var songPanel = songPanelList[i].GetComponent<SongInfo>();
            var song = StageManager.Instance.stageList[i].stageSong;
            songPanel.backgroundImage.sprite = song.background;
            songPanel.albumArtImage.sprite = song.albumArt;
        }
    }

    public void ChoiceSong()
    {
        for(int i = 0; i < songPanelList.Count; i++)
        {
            if (scrollRect.content.anchoredPosition.y == panelDistance * i)
            {
                var stage = StageManager.Instance.stageList[i];
                StageManager.Instance.currentStageNum = i;
                StageManager.Instance.songName.text = stage.stageSong.songName;
                StageManager.Instance.artistName.text = stage.stageSong.artistName;
                if(previewSong.clip != stage.previewSong)
                {
                    previewSong.clip = stage.previewSong;
                    previewSong.Play();
                }
            }
        }
    }
}
