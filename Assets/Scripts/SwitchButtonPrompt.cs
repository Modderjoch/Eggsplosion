using Steamworks;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwitchButtonPrompt: MonoBehaviour
{
    public Sprite keyboardInput;
    public Sprite playstationInput;
    public Sprite xboxInput;
    public Sprite nintendoInput;
    public Sprite steamDeckInput;

    private MenuHandler menuHandler;
    private string currentController;
    private Image inputImage;

    private void Awake()
    {
        menuHandler = GameObject.Find("UIManager").GetComponent<MenuHandler>();
        menuHandler.AddPrompt(gameObject);
        if (menuHandler.lastGamepad != null) { currentController = menuHandler.lastGamepad.displayName; }

        inputImage = gameObject.GetComponent<Image>();

        SteamInput.RunFrame();

        InputHandle_t inputHandle = SteamInput.GetControllerForGamepadIndex(menuHandler.lastGamepadIndex);
        ESteamInputType inputTypeSteam = SteamInput.GetInputTypeForHandle(inputHandle);

        string filePath = "log.txt"; // Replace this with the desired file path

        // Open the file in append mode
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(inputTypeSteam.ToString() + " connected on: " + Time.time + "controller is nr: " + menuHandler.lastGamepadIndex + "\n" + "this is the first controller");
        }

        switch (inputTypeSteam)
        {
            //Xbox Input Prompts
            case ESteamInputType.k_ESteamInputType_XBox360Controller:
            case ESteamInputType.k_ESteamInputType_XBoxOneController:
                inputImage.sprite = xboxInput;
                break;
            //Nintendo Input Prompts
            case ESteamInputType.k_ESteamInputType_SwitchJoyConSingle:
            case ESteamInputType.k_ESteamInputType_SwitchJoyConPair:
            case ESteamInputType.k_ESteamInputType_SwitchProController:
                inputImage.sprite = nintendoInput;
                break;
            //Playstation Input Prompts
            case ESteamInputType.k_ESteamInputType_PS3Controller:
            case ESteamInputType.k_ESteamInputType_PS4Controller:
            case ESteamInputType.k_ESteamInputType_PS5Controller:
                inputImage.sprite = playstationInput;
                break;
            //Steam Input Prompts
            case ESteamInputType.k_ESteamInputType_SteamController:
            case ESteamInputType.k_ESteamInputType_SteamDeckController:
                inputImage.sprite = steamDeckInput;
                break;
            case ESteamInputType.k_ESteamInputType_Unknown:
                inputImage.GetComponent<SpriteRenderer>().sprite = null;
                break;
            default:
                //Debug.Log("Default case");
                inputImage.sprite = xboxInput;
                break;
        }
    }
}
