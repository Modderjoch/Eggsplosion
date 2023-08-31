using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject leaderButton;
    [SerializeField] private GameObject leaderOpen;
    [SerializeField] private GameObject leaderClose;
    [SerializeField] private GameObject leaderNext;
    [SerializeField] private GameObject leaderPrevious;

    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject inGameContinue;

    [SerializeField] private Button eggsplanationButton;
    [SerializeField] private GameObject backButton;

    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private GameObject eventsCanvas;

    [SerializeField] private List<GameObject> buttonPrompts = new List<GameObject>();
    [HideInInspector] public Gamepad lastGamepad;
    [HideInInspector] public int lastGamepadIndex;

    [SerializeField] private GameObject[] leaderboardButtons;
    private int lastSelectedBoard = 0;

    private bool keyboardUsed = false;
    private bool isPaused = false;

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
        if (eggsplanationButton != null)
            eggsplanationButton.onClick.Invoke();
        ClickSound();
        DetectInputDevice();
    }

    public void Cancel()
    {
        backButton = GameObject.FindGameObjectWithTag("BackButton");

        if (backButton == null)
        {
            backButton = GameObject.Find("BackButton");
        }

        Debug.Log("Cancel");
        DetectInputDevice();
        backButton.GetComponent<Button>().onClick.Invoke();
    }

    public void Options()
    {
        if (!isPaused)
        {
            EventSystem eventSystem = EventSystem.current;

            inGameCanvas.SetActive(true);
            eventSystem.SetSelectedGameObject(inGameContinue);
            Time.timeScale = 0f;
            Debug.Log("Game Paused");

            isPaused = true;
        }
        else
        {
            EventSystem eventSystem = EventSystem.current;

            inGameCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(null);
            Time.timeScale = 1.0f;
            Debug.Log("Game unpaused");

            isPaused = false;
        }
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

        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (leaderNext != null)
            {
                leaderNext.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                Debug.Log("No next leaderboard button found");
            }
        }
        else
        {
            if (GameObject.Find("AddRound") != null)
            {
                GameObject.Find("AddRound").GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                Debug.Log("No add round button found");
            }
        }
    }

    public void SubtractRound()
    {
        DetectInputDevice();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (leaderPrevious != null)
            {
                leaderPrevious.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                Debug.Log("No next leaderboard button found");
            }
        }
        else
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
    }

    public void NextBoard()
    {
        DetectInputDevice();

        if (leaderboard.activeSelf)
        {
            if(lastSelectedBoard == leaderboardButtons.Length - 1)
            {
                lastSelectedBoard = 0;
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[lastSelectedBoard].gameObject);
                leaderboardButtons[lastSelectedBoard].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                lastSelectedBoard++;
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[lastSelectedBoard].gameObject);
                leaderboardButtons[lastSelectedBoard].GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            Debug.Log("Leaderboard is not active");
        }
    }

    public void PreviousBoard()
    {
        DetectInputDevice();

        if (leaderboard.activeSelf)
        {
            if (lastSelectedBoard == 0)
            {
                lastSelectedBoard = leaderboardButtons.Length-1;
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[lastSelectedBoard].gameObject);
                leaderboardButtons[lastSelectedBoard].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                lastSelectedBoard--;
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[lastSelectedBoard].gameObject);
                leaderboardButtons[lastSelectedBoard].GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            Debug.Log("Leaderboard is not active");
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
        SteamInput.RunFrame();

        var gamepads = Gamepad.all.ToArray();

        // Iterate over all connected gamepads
        for (int i = 0; i < gamepads.Length; i++)
        {
            var gamepad = gamepads[i];

            if (gamepad.wasUpdatedThisFrame)
            {
                lastGamepad = gamepad;
                lastGamepadIndex = i;
                keyboardUsed = false; // Reset keyboard usage flag when a gamepad is used
            }
            else if (Keyboard.current.wasUpdatedThisFrame)
            {
                lastGamepad = null; // Reset lastGamepad when the keyboard is used
                keyboardUsed = true;
            }
        }

        if (lastGamepad != null)
        {
            //Debug.Log("Last input device used: " + lastGamepad.displayName);
            SwitchUI(lastGamepadIndex);
        }
    }

    private void SwitchUI(int gamepadIndex)
    {
        InputHandle_t inputHandle = SteamInput.GetControllerForGamepadIndex(gamepadIndex);
        ESteamInputType inputTypeSteam = SteamInput.GetInputTypeForHandle(inputHandle);

        string filePath = "log.txt"; // Replace this with the desired file path

        // Open the file in append mode
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Saves/" + filePath, true))
        {
            writer.WriteLine(inputTypeSteam.ToString() + " connected on: " + Time.time + "controller is nr: " + gamepadIndex);
        }

        for (int i = 0; i < buttonPrompts.Count; i++)
        {
            //Debug.Log("Switching UI");

            if (buttonPrompts[i] != null)
            {
                GameObject prompt = buttonPrompts[i];
                Image image = prompt.GetComponent<Image>();
                SwitchButtonPrompt switchPrompt = prompt.GetComponent<SwitchButtonPrompt>();

                switch (inputTypeSteam)
                {
                    //Xbox Input Prompts
                    case ESteamInputType.k_ESteamInputType_XBox360Controller:
                    case ESteamInputType.k_ESteamInputType_XBoxOneController:
                        image.sprite = switchPrompt.xboxInput;
                        break;
                    //Nintendo Input Prompts
                    case ESteamInputType.k_ESteamInputType_SwitchJoyConSingle:
                    case ESteamInputType.k_ESteamInputType_SwitchJoyConPair:
                    case ESteamInputType.k_ESteamInputType_SwitchProController:
                        image.sprite = switchPrompt.nintendoInput;
                        break;
                    //Playstation Input Prompts
                    case ESteamInputType.k_ESteamInputType_PS3Controller:
                    case ESteamInputType.k_ESteamInputType_PS4Controller:
                    case ESteamInputType.k_ESteamInputType_PS5Controller:
                        image.sprite = switchPrompt.playstationInput;
                        break;
                    //Steam Input Prompts
                    case ESteamInputType.k_ESteamInputType_SteamController:
                    case ESteamInputType.k_ESteamInputType_SteamDeckController:
                        image.sprite = switchPrompt.steamDeckInput;
                        break;
                    case ESteamInputType.k_ESteamInputType_Unknown:
                        image.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    default:
                        //Debug.Log("Default case");
                        image.sprite = switchPrompt.xboxInput;
                        break;
                }
            }
        }
    }
}
