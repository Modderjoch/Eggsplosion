using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SaveOptions : MonoBehaviour
{
    public List<Slider> volumeSliders = new List<Slider>();
    public AudioMixer audioMixer;

    private static SaveOptions instance;
    public static SaveOptions Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddSlider(Slider slider)
    {
        if (volumeSliders.Contains(slider))
        {
            Debug.Log("Already known");
        }
        else
        {
            volumeSliders.Add(slider);
        }
    }

    public void SetVolumes()
    {
        foreach (Slider slider in volumeSliders)
        {
            Debug.Log(slider.name + ": " + slider.value);

            float savedValue = PlayerPrefs.GetFloat(slider.name, 1f);

            //Debug.Log("set the value to: " + Mathf.Log10(savedValue) * 20 + " original value was: " + savedValue);
            audioMixer.SetFloat(slider.name, Mathf.Log10(savedValue) * 20);
            slider.value = savedValue;

            Debug.Log(slider.name + " / " + PlayerPrefs.GetFloat(slider.name));
        }
    }

    public void ClearSliders()
    {
        volumeSliders.Clear();
    }
}
