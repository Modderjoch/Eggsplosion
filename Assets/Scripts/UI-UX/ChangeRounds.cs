using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeRounds : MonoBehaviour
{
    [SerializeField]
    public TMP_Text maxRoundText;
    public string[] roundOptions;

    private int selectedOption;
    public bool numberChanged;
    void Start()
    {
    }

    public void SetNumberChanged()
    {
        numberChanged = true;
    }

    public void NextCharacter()
    {
        selectedOption = (selectedOption + 1) % roundOptions.Length;
        maxRoundText.text = roundOptions[selectedOption];
    }

    public void PreviousCharacter()
    {       
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption += roundOptions.Length;
        }
        maxRoundText.text = roundOptions[selectedOption];
    }
    public void ChangeMaxRounds()
    {
        PlayerConfigurationManager.Instance.maxAmountOfRounds =  int.Parse(maxRoundText.text);
        Debug.Log(PlayerConfigurationManager.Instance.maxAmountOfRounds);
    }
}
