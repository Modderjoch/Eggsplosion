using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private void Start()
    {
        if (AudioManager.Instance.IsSoundPlaying("MenuMusic"))
        {
            return;
        }
        else if(AudioManager.Instance.IsSoundPlaying("GameMusic0") || AudioManager.Instance.IsSoundPlaying("GameMusic1") || AudioManager.Instance.IsSoundPlaying("GameMusic2"))
        {
            AudioManager.Instance.StopAllMusic();
            AudioManager.Instance.Play("MenuMusic");
        }
        else
        {
            AudioManager.Instance.Play("MenuMusic");
        }
    }
}
