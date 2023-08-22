using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [SerializeField] private List<GameObject> buttonPrompts = new List<GameObject>();
    [HideInInspector] public Gamepad lastGamepad;
    private bool keyboardUsed = false;

    public void Leaderboard()
    {
        DetectInputDevice();

        if (leaderboard != null)
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
        DetectInputDevice();

        if (eggsplanationButton != null)
        eggsplanationButton.onClick.Invoke();
        ClickSound();
    }

    public void Cancel()
    {
        DetectInputDevice();

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
        DetectInputDevice();

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
        DetectInputDevice();

        if (GameObject.Find("AddRound") != null)
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
        DetectInputDevice();

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

    public void AddPrompt(GameObject prompt)
    {
        buttonPrompts.Add(prompt);
    }
    public void ClearPrompts()
    {
        buttonPrompts.Clear();
    }

    public void DetectInputDevice()
    {
        var gamepads = Gamepad.all;

        // Iterate over all connected gamepads
        foreach (var gamepad in gamepads)
        {
            if (gamepad.wasUpdatedThisFrame)
            {
                lastGamepad = gamepad;
                keyboardUsed = false; // Reset keyboard usage flag when a gamepad is used
            }
        }

        // Check if any keyboard key was pressed this frame
        if (Keyboard.current.wasUpdatedThisFrame)
        {
            lastGamepad = null; // Reset lastGamepad when the keyboard is used
            keyboardUsed = true;
        }

        // Output the last input device used
        if (keyboardUsed)
        {
            Debug.Log("Last input device used: Keyboard");
            SwitchUI("Keyboard");
        }
        else if (lastGamepad != null)
        {
            Debug.Log("Last input device used: " + lastGamepad.displayName);
            SwitchUI(lastGamepad.displayName);
        }
    }

    private void SwitchUI(string inputType)
    {
        for (int i = 0; i < buttonPrompts.Count; i++)
        {
            Debug.Log("Switching UI");

            if (buttonPrompts[i] != null)
            {
                GameObject prompt = buttonPrompts[i];
                Image image = prompt.GetComponent<Image>();
                SwitchButtonPrompt switchPrompt = prompt.GetComponent<SwitchButtonPrompt>();

                switch (inputType)
                {
                    case "Keyboard":
                        image.sprite = switchPrompt.keyboardInput;
                        break;
                    case "DualSense Wireless Controller":
                        image.sprite = switchPrompt.playstationInput;
                        break;
                    case "Nintendo Switch Pro Controller":
                        image.sprite = switchPrompt.nintendoInput;
                        break;
                    case "Xbox Controller":
                        image.sprite = switchPrompt.xboxInput;
                        break;
                    default:
                        Debug.Log("Default case");
                        image.sprite = switchPrompt.xboxInput;
                        break;
                }
            }            
        }
    }
}
