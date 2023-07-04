using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance { get; private set; }

    AudioSource defaultAudioSettings;
    public AudioMixerGroup mixerChannel; // or like this...?

    void Awake()
    {
        defaultAudioSettings = GetComponent<AudioSource>();
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
    

            s.source.outputAudioMixerGroup = defaultAudioSettings.outputAudioMixerGroup;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

     public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    //FindObjectOfType<AudioManager>().Play("Name");
}
