using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds = new List<Sound>();
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            InitializeAudioSources();
        }

    }

    private void InitializeAudioSources()
    {
        foreach (Sound sound in sounds)
        {
            // Only create audio sources if they haven't been created already
            if (sound.source == null)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.mixerChannel;
            }
        }
    }

    public void Play(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        sound.source.Play();
    }

     public void Stop(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        sound.source.Stop();
    }

    public bool IsSoundPlaying(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }

        return sound.source.isPlaying;
    }

    private void Update()
    {
        if (IsSoundPlaying("MenuMusic"))
        {
            return;
        }
        else if (IsSoundPlaying("GameMusic0"))
        {
            return;
        }
        else if (IsSoundPlaying("GameMusic1"))
        {
            return;
        }
        else if (IsSoundPlaying("GameMusic2"))
        {
            return;
        }
        else if (!IsSoundPlaying("GameMusic0") || !IsSoundPlaying("GameMusic1") || !IsSoundPlaying("GameMusic2"))
        {
            StartRandomMusicTrack();
        }
    }

    public void StartRandomMusicTrack()
    {
        int newTrack = UnityEngine.Random.Range(0, 3);
        string trackToPlay = string.Format("GameMusic{0}", newTrack);

        Play(trackToPlay);
    }

    public void StopAllMusic()
    {
        Stop("GameMusic0");
        Stop("GameMusic1");
        Stop("GameMusic2");
    }
}
