// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class TimerUISpeed : MonoBehaviour
// {
//     float currentTimeSpeed;
//     public float startingTime = 4f;

//     public bool TimerPlaying;

//     [SerializeField] TMP_Text countdownTextSpeed;
//     void Start()
//     {
//         currentTimeSpeed = startingTime;
//         TimerPlaying = true;
//     }

//     void Update()
//     {
//         currentTimeSpeed -= 1 * Time.deltaTime;
//         countdownTextSpeed.text = currentTimeSpeed.ToString("0");

//         if (currentTimeSpeed <= 0 && TimerPlaying)
//         {
//             currentTimeSpeed = 4;
//             DisableTimer();
//             TimerPlaying = false;
//         }
//     }

//     public void DisableTimer()
//     {
//         currentTimeSpeed = 4;
//         PlayerStats playerStats = GetComponentInParent<PlayerStats>();
//         playerStats.TurnOff();
//     }
// }