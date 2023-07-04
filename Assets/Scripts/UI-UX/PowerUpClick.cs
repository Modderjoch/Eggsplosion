using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpClick : MonoBehaviour
{
    public GameObject[] canvasses;

    private int selectedOption;
    public bool numberChanged;

    public void SetNumberChanged()
    {
        numberChanged = true;
    }

    public void NextCharacter()
    {
        canvasses[selectedOption].gameObject.SetActive(false);
        selectedOption = (selectedOption + 1) % canvasses.Length;
        canvasses[selectedOption].gameObject.SetActive(true);
    }

    public void PreviousCharacter()
    {
        canvasses[selectedOption].gameObject.SetActive(false);
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption += canvasses.Length;
        }
        canvasses[selectedOption].gameObject.SetActive(true);
    }
}
