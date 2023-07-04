using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public static MainMenuMusic Instance { get; private set; }
    private string currentSceneName;
    public AudioSource audioSource;

    private void Awake()
    {

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(audioSource);

        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
            Destroy(audioSource);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        if (!audioSource.isPlaying)
        {
            Debug.Log("is not playing!");
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
       
        currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName == "LevelDesign1" || currentSceneName == "Level2v2" || currentSceneName == "Farm Level")
        {
            Debug.Log("Level active");
            Destroy(gameObject);
        }
    }
}
