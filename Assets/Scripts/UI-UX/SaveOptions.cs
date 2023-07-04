using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveOptions : MonoBehaviour
{
    
    public TMP_Dropdown dropDownMenuAudio;
    public TMP_Dropdown dropDownMenuPostProcessing;
    public AudioMixerGroup[] audioMixerGroups;
    public PostProcessProfile[] postProcessProfiles;
    public static SaveOptions Instance { get; private set; }
    AudioMixerGroup savedAudioMixer;
    PostProcessProfile postProcessProfile;

    private string sceneName;
    private Camera sceneCam;
    private AudioManager audioManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }


        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "LevelDesign1" || sceneName == "Level2v2")
        {
            audioManager = GameObject.FindGameObjectWithTag("GameAudio").GetComponent<AudioManager>();
            audioManager.mixerChannel = savedAudioMixer;
            sceneCam = GameObject.FindGameObjectWithTag("GameCam").GetComponent<Camera>();
            sceneCam.GetComponent<PostProcessProfile>().settings = postProcessProfile.settings;
        }
    }
    public void SetSavedAudioMixer(AudioMixerGroup mixer)
    {
        savedAudioMixer = mixer;
    }

    public void SetSavedPostProcessProfile(PostProcessProfile profile)
    {
        postProcessProfile = profile;
    }

    public void ChangeAudioMixer()
    {
        if (dropDownMenuAudio.value == 0)
        {
            SetSavedAudioMixer(audioMixerGroups[0]);
        }
        else if (dropDownMenuAudio.value == 1)
        {
            SetSavedAudioMixer(audioMixerGroups[1]);
        }
        else if (dropDownMenuAudio.value == 2)
        {
            SetSavedAudioMixer(audioMixerGroups[2]);
        }
    }

    public void ChangePostProcessingProfile()
    {
        if (dropDownMenuPostProcessing.value == 0)
        {
            SetSavedPostProcessProfile(postProcessProfiles[0]);
        }
        else if (dropDownMenuPostProcessing.value == 1)
        {
            SetSavedPostProcessProfile(postProcessProfiles[1]);
        }
        else if (dropDownMenuPostProcessing.value == 2)
        {
            SetSavedPostProcessProfile(postProcessProfiles[2]);
        }
    }
}
