using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickManager : MonoBehaviour
{
    public void OnClickStartButton() => SceneManager.LoadScene("TestScene");
    public void OnClickExitButton() => Application.Quit();
    public void OnClickPlayButton() => SceneManager.LoadScene("PlayScene");
}
