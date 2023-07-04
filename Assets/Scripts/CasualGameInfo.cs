using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CasualGameInfo : MonoBehaviour
{
    public static CasualGameInfo instance { get; private set; }
    public bool[] disableList;
    void Awake()
    {     
        if (instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void DisablePowerUpButton(int number)
    {
        if (disableList[number] == false)
        {
            disableList[number] = true;
        }
        else
        {
            disableList[number] = false;

        }
    }
    public void changeButtonColor(GameObject button)
    {
        Image buttonImg = button.GetComponent<Image>();
        if (buttonImg.color == Color.white)
        {
            
            buttonImg.color = Color.red;
        }
        else
        {
          
            buttonImg.color = Color.white;
        }
    }

    public void LoadLevel(string input)
    {
        SceneManager.LoadScene(input);
    }
}
