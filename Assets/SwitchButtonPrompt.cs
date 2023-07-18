using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwitchButtonPrompt: MonoBehaviour
{
    public Sprite keyboardInput;
    public Sprite playstationInput;
    public Sprite xboxInput;
    public Sprite nintendoInput;

    private MenuHandler menuHandler;
    private string currentController;
    private Image inputImage;

    private void Awake()
    {
        menuHandler = GameObject.Find("UIManager").GetComponent<MenuHandler>();
        menuHandler.AddPrompt(gameObject);
        currentController = menuHandler.lastGamepad.displayName;

        inputImage = gameObject.GetComponent<Image>();

        switch(currentController)
        {
            case "Keyboard":
                inputImage.sprite = keyboardInput;
                break;
            case "DualSense Wireless Controller":
                inputImage.sprite = playstationInput;
                break;
            case "Nintendo Switch Pro Controller":
                inputImage.sprite = nintendoInput;
                break;
            case "Xbox Controller":
                inputImage.sprite = xboxInput;
                break;
            default:
                Debug.Log("Default case");
                inputImage.sprite = xboxInput;
                break;
        }
    }
}
