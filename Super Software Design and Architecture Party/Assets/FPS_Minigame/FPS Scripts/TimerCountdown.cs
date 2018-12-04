using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{

    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool gameEnd = false;

    void Start()
    {
        timer = mainTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }
        else if (timer < 0.1f && gameEnd)
        {
            canCount = false;
            gameEnd = false;
            uiText.text = "0.00";
            timer = 0.0f;
        }
    }
}
