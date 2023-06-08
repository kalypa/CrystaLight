using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private float setTime = 5.0f;
    public AudioSource music;
    private void Start()
    {
        setTime = 5;
        countDownText.text = setTime.ToString();
    }
    void Update()
    {
        if (setTime > 0) setTime -= Time.unscaledDeltaTime / 1.9f;
        else
        {
            Time.timeScale = 1;
            setTime = 5;
            countDownText.gameObject.SetActive(false);
            music.UnPause();
        }

        countDownText.text = Mathf.Round(setTime).ToString();
    }
}
