using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    float currentTime;
    public float startingTime = 5f;

    public bool TimerPlaying;

    [SerializeField] TMP_Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        TimerPlaying = true;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0 && TimerPlaying)
        {
            currentTime = 5;
            DisableTimer();
            TimerPlaying = false;
        }
    }

    public void DisableTimer()
    {
        currentTime = 5;
        PlayerStats playerStats = GetComponentInParent<PlayerStats>();
        playerStats.TurnOff();
    }
}