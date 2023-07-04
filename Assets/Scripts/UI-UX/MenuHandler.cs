using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject leaderButton;
    [SerializeField] private GameObject leaderOpen;
    [SerializeField] private GameObject leaderClose;

    [SerializeField] private Button eggsplanationButton;
    [SerializeField] private GameObject backButton;

    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private GameObject eventsCanvas;

    public void Leaderboard()
    {
        if(leaderboard != null)
        {
            if (leaderboard.activeSelf)
            {
                leaderboard.SetActive(false);
                leaderOpen.SetActive(true);
                leaderButton.SetActive(true);
                leaderClose.SetActive(false);
            }
            else
            {
                leaderboard.SetActive(true);
                leaderOpen.SetActive(false);
                leaderButton.SetActive(false);
                leaderClose.SetActive(true);
            }
        }

        ClickSound();
    }

    public void Eggsplanation()
    {
        eggsplanationButton.onClick.Invoke();
        ClickSound();
    }

    public void Cancel()
    {
        backButton = GameObject.FindGameObjectWithTag("BackButton");

        if(backButton == null)
        {
            backButton = GameObject.Find("BackButton");
        }

        Debug.Log("Cancel");
        backButton.GetComponent<Button>().onClick.Invoke();
    }

    public void EnableTabs(string name)
    {
        switch (name)
        {
            case "control":
                controlsCanvas.SetActive(true);
                storyCanvas.SetActive(false);
                powerupCanvas.SetActive(false);
                eventsCanvas.SetActive(false);
                break;
            case "story":
                controlsCanvas.SetActive(false);
                storyCanvas.SetActive(true);
                powerupCanvas.SetActive(false);
                eventsCanvas.SetActive(false);
                break;
            case "powerup":
                controlsCanvas.SetActive(false);
                storyCanvas.SetActive(false);
                powerupCanvas.SetActive(true);
                eventsCanvas.SetActive(false);
                break;
            case "event":
                controlsCanvas.SetActive(false);
                storyCanvas.SetActive(false);
                powerupCanvas.SetActive(false);
                eventsCanvas.SetActive(true);
                break;
            default:
                Debug.Log("Could not disable/enable canvasses");
                break;
        }
    }

    public void AddRound()
    {
        if(GameObject.Find("AddRound") != null)
        {
            GameObject.Find("AddRound").GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            Debug.Log("No add round button found");
        }
    }

    public void SubtractRound()
    {
        if (GameObject.Find("SubtractRound") != null)
        {
            GameObject.Find("SubtractRound").GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            Debug.Log("No subtract round button found");
        }
    }

    public void ClickSound()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
