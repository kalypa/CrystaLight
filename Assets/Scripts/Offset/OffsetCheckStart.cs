using UnityEngine;

public class OffsetCheckStart : MonoBehaviour
{
    void Update() => KeyDownCheck();

    void CheckStart() => OffsetSongManager.Instance.StartSong();

    public void KeyDownCheck()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)) CheckStart();
        else if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
