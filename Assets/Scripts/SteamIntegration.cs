using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    void Start()
    {
        try
        {
           Steamworks.SteamClient.Init(1231231231); //getnumber from oebe    
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }


    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
