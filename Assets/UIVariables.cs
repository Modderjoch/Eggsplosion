using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIVariables : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject leaderButton;
    [SerializeField] private GameObject leaderOpen;
    [SerializeField] private GameObject leaderClose;
    [SerializeField] private Button eggsplanationButton;
    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject inGameContinue;

    private MenuHandler menuHandler;

    protected void Awake()
    {
        AssignVariables();
    }

    private void AssignVariables()
    {
        menuHandler = UIInputManager.Instance.GetComponent<MenuHandler>();

        if (menuHandler != null)
        {
            if (leaderboard != null)
            {
                menuHandler.leaderboard = leaderboard;
                menuHandler.leaderButton = leaderButton;
                menuHandler.leaderOpen = leaderOpen;
                menuHandler.leaderClose = leaderClose;
                menuHandler.eggsplanationButton = eggsplanationButton;
                menuHandler.playButton = playButton;
            }
            else if (inGameCanvas != null)
            {
                menuHandler.inGameCanvas = inGameCanvas;
                menuHandler.inGameContinue = inGameContinue;
            }
        }
    }

    public void OptionsOut()
    {
        EventSystem eventSystem = EventSystem.current;

        inGameCanvas.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        Time.timeScale = 1.0f;
        Debug.Log("Game unpaused");

        menuHandler.isPaused = false;
    }
}
