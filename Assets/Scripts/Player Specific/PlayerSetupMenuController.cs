using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private GameObject teamMenuPanel;
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject mainInputField;

    public bool isTeamGameMode = false;

    public Button[] colorButtons;

    public AnimatorOverrideController[] overRides;
    public Sprite[] sprites;


    private int selectedButton = 0;

    private float ignoreImputTime = 1.5f;
    private bool inputEnabled;

    private Navigation inputFieldNavigation; // make global variable to avoid code repition 
    private void Awake()
    {
        inputFieldNavigation = mainInputField.GetComponent<InputField>().navigation;      
    }
    private void HandleInputFieldNavSetup()
    {
        mainInputField.GetComponent<InputField>().navigation = ChangeNavigation(inputFieldNavigation, colorButtons[selectedButton]);
    }

    private Navigation ChangeNavigation(Navigation nav, Button button) // make method to avoid code repition 
    {
        nav.selectOnUp = button;
        nav.selectOnLeft = button;
        nav.selectOnRight = button;
        nav.selectOnDown = readyButton;
        return nav;
    }

    public int GetCurrentEnabledSprite()
    {
        return selectedButton;
    }
    public void SetPlayerIndex(int p1)
    {
        playerIndex = p1;
        titleText.SetText("Player" + (playerIndex +1).ToString());
        ignoreImputTime = Time.time + ignoreImputTime;
    }

    void Update()
    {
        HandleInputFieldNavSetup();
        if (Time.time > ignoreImputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetName()
    {
        if (!inputEnabled) { return; }
        string name = mainInputField.GetComponentInChildren<Text>().text;
        titleText.SetText(name);
        PlayerConfigurationManager.Instance.SetPlayerName(playerIndex, name);
    }
    public void SetSprite(Sprite sprite)
    {
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.SetPlayerSprite(playerIndex, sprite);
        readyPanel.SetActive(true);
        readyButton.interactable = true;
        menuPanel.SetActive(false);
        readyButton.Select();
    }
    public void SetSpriteId(int id)
    {
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.SetPlayerSpriteId(playerIndex, id);
    }
    public void SetTeam(bool isBlue)
    {
        PlayerConfigurationManager.Instance.SetTeam(playerIndex, isBlue);

    }
    public void SetAnimator(AnimatorOverrideController animatorOverrideController)
    {
        PlayerConfigurationManager.Instance.SetAnimator(playerIndex, animatorOverrideController);
    }
    public void NextCharacter()
    {
        colorButtons[selectedButton].gameObject.SetActive(false);
        selectedButton = (selectedButton + 1) % colorButtons.Length;
        colorButtons[selectedButton].gameObject.SetActive(true);
    }

    public void PreviousCharacter()
    {
        colorButtons[selectedButton].gameObject.SetActive(false);
        selectedButton--;
        if (selectedButton < 0)
        {
            selectedButton += colorButtons.Length;
        }
        colorButtons[selectedButton].gameObject.SetActive(true);
    }
    public void ReadyPlayer()
    {
        if (isTeamGameMode)
        {
            if (GetCurrentEnabledSprite() == 0)
            {
                SetTeam(true);
            }
            else
            {
                SetTeam(false);
            }
        }
        SetSprite(sprites[GetCurrentEnabledSprite()]);
        SetAnimator(overRides[GetCurrentEnabledSprite()]);
        SetSpriteId(GetCurrentEnabledSprite());
        Debug.Log(GetCurrentEnabledSprite());
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        //SetName();
        readyButton.gameObject.SetActive(false);
    }
}
