using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour, ISelectHandler, IDeselectHandler, ICancelHandler
{
    public string level;
    public string mixer;
    public GameObject confirmationButton;
    public bool destroyCasualInfoOnAwake = false;

    public GameObject[] selectables;
    private string selected;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Start()
    {
        if(GetComponent<Slider>() != null)
        {
            SaveOptions.Instance.AddSlider(GetComponent<Slider>());
            SaveOptions.Instance.SetVolumes();
            SaveOptions.Instance.ClearSliders();
        }

        if (destroyCasualInfoOnAwake)
        {
            GameObject casualManager = GameObject.FindGameObjectWithTag("CasualInfo");
            Destroy(casualManager);
        }
    }

    public void DestroyPlayerConfigManager()
    {
        PlayerConfigurationManager.Instance.SelfDestroy();
    }

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
        Debug.Log("OnCancel");
        eventData.selectedObject = selectables[4];
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log(this.ButtonGameObject.name + " was deselected");
    }

    public void SetSelectedObject(BaseEventData eventData, GameObject button)
    {
        eventData.selectedObject = button;
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
        //EditorApplication.isPlaying = false;

    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat(mixer, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(mixer, sliderValue);
        PlayerPrefs.Save();
    }

    public void SetString(string toSelect)
    {
        Debug.Log("Changed " + selected + " to: " + toSelect);
        selected = toSelect;
    }

    public void GoBack()
    {
        EventSystem eventSystem = EventSystem.current;

        selected = "Back";
        eventSystem.SetSelectedGameObject(selectables[4]);
    }

    public void SwitchSelected(BaseEventData eventData)
    {
        switch(selected)
        {
            case "Sound":
                eventData.selectedObject = selectables[0];
                break;
            case "Graphics":
                eventData.selectedObject = selectables[1];
                break;
            case "Controls":
                eventData.selectedObject = selectables[2];
                break;
            case "General":
                eventData.selectedObject = selectables[3];
                break;
            case "Back":
                Debug.Log("Switch to default panel");
                eventData.selectedObject = selectables[4];
                break;
            default:
                Debug.Log("No given case");
                break;
        }
    }
}
