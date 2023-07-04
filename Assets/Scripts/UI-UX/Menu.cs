using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.Events;

public class Menu : MonoBehaviour, ISelectHandler, IDeselectHandler, ICancelHandler
{
    public string level;
    public GameObject confirmationButton;

    [SerializeField]
    private AudioMixer audioMixer;

    public void ClickSound()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    public void OnSelect(BaseEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("MenuHover");
    }

    public void OnCancel(BaseEventData eventData)
    {
    //    backButton = GameObject.FindGameObjectWithTag("BackButton");
    //    Debug.Log("Cancel");
    //    eventData.selectedObject = backButton;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log(this.ButtonGameObject.name + " was deselected");
    }

    public void Ready()
    {
        FindObjectOfType<AudioManager>().Play("ReadyClick");
    }

    public void LoadLevelOne()
    {
        GameObject configManager = GameObject.FindGameObjectWithTag("GameController");
        Destroy(configManager);
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        GameObject configManager = GameObject.FindGameObjectWithTag("GameController");
        Destroy(configManager);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}