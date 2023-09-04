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
        else if(AudioManager.Instance.IsSoundPlaying("GameMusic"))
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
