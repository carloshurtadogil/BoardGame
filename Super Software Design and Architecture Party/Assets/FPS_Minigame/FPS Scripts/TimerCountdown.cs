using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TimerCountdown : NetworkBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float maxTime;

    [SerializeField] GameObject scoreboard;
    [SerializeField] GameObject timerboard;


    private float timer;
    private bool canCount = true;

    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.01f && canCount)
        {
            timer -= Time.deltaTime;
            UpdateTimeText(timer);
        }
        else if (timer <= 0.06f)
        {
            canCount = false;
            uiText.text = "00.00";
            timer = 0.0f;
            timerboard.SetActive(false);
            TimesUp();
        }
    }

    void UpdateTimeText(float timer)
    {
        if (timer < 10.00f)
            uiText.text = "0" + timer.ToString("F");
        else
            uiText.text = timer.ToString("F");
    }

    void TimesUp()
    {
        scoreboard.SetActive(true);
        Time.timeScale = 0f;
    }
}


