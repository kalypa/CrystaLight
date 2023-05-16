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
    private float distancehalf = 540f;
    public AudioSource previewSong;
    private void Awake() => Instance = this;

    private void Start() => Init();
    void Init()
    {
        SongInfoInit();
        ChoiceSong(false);
    }
    void SongInfoInit()
    {
        for(int i = 0; i < songPanelList.Count; i++) 
        {
            var songPanel = songPanelList[i].GetComponent<SongInfo>();
            var song = StageManager.Instance.stageList[i].stageSong;
            songPanel.albumArtImage.sprite = song.albumArt;
        }
    }

    void stageSetting(int i, bool isDrag)
    {
        var instance = StageManager.Instance;
        var stage = instance.stageList[i];
        instance.currentStageNum = i;
        instance.songName.text = stage.stageSong.songName;
        instance.artistName.text = stage.stageSong.artistName;
        if (!isDrag)
        {
            if (previewSong.clip != stage.previewSong)
            {
                previewSong.clip = stage.previewSong;
                previewSong.Play();
            }
        }
    }

    void ClampedCheck(bool isClampPos, int i, bool isDrag)
    {
        if (isClampPos) stageSetting(i, isDrag);
    }

    public void ChoiceSong(bool isDrag)
    {
        var contentPos = scrollRect.content.anchoredPosition.y;
        for (int i = 0; i < songPanelList.Count; i++)
        {
            var isClampPosMin = contentPos > panelDistance * (i - 1) + distancehalf;
            var isClampPosMax = contentPos <= panelDistance * (i + 1) - distancehalf;
            if (i == 0)
            {
                if(isClampPosMax) ClampedCheck(isClampPosMax, i, isDrag);
            }
            else if (i == songPanelList.Count - 1)
            {
                if(isClampPosMin) ClampedCheck(isClampPosMin, i, isDrag);
            }
            else
            {
                if(isClampPosMin && isClampPosMax) ClampedCheck(isClampPosMin && isClampPosMax, i, isDrag);
            }
        }
    }
}
