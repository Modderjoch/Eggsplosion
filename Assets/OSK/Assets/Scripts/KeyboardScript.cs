using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardScript : MonoBehaviour
{
    public InputField TextField;
    public GameObject EngLayoutSml, EngLayoutBig, KeyboardButton, aButton, ReadyButton, CapsButtonLow, CapsButtonBig;

    public void alphabetFunction(string alphabet)
    {
        TextField.text=TextField.text + alphabet;
    }

    public void BackSpace()
    {

        if(TextField.text.Length>0) TextField.text= TextField.text.Remove(TextField.text.Length-1);

    }

    public void CloseAllLayouts()
    {
        EngLayoutSml.SetActive(false);
        EngLayoutBig.SetActive(false);
        KeyboardButton.SetActive(false);
    }

    public void SwitchLayout()
    {
        if (EngLayoutSml.activeSelf)
        {
            EngLayoutSml.SetActive(false);
            EngLayoutBig.SetActive(true);
        }
        else
        {
            EngLayoutBig.SetActive(false);
            EngLayoutSml.SetActive(true);
        }
    }

    public void ShowLayout(GameObject SetLayout)
    {
        CloseAllLayouts();
        SetLayout.SetActive(true);
    }

    public void Caps(BaseEventData eventData)
    {
        if (EngLayoutSml.activeSelf)
        {
            eventData.selectedObject = CapsButtonLow;
        }
        else
        {
            eventData.selectedObject = CapsButtonBig;
        }
    }

    public void EnableKeyboard(BaseEventData eventData)
    {
        CloseAllLayouts();
        ShowLayout(EngLayoutSml);
        eventData.selectedObject = aButton;
    }

    public void DisableKeyboard(BaseEventData eventData)
    {
        CloseAllLayouts();
        ShowLayout(KeyboardButton);
        eventData.selectedObject = ReadyButton;
    }
}
