using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HostControllerLog : MonoBehaviour
{
    public InputDevice hostDevice;
    public int hostGamepadID;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (hostDevice == null)
        {
            hostDevice = Gamepad.current;
            hostGamepadID = hostDevice.deviceId;
            if (hostDevice.deviceId != null)
            {   
                Debug.Log(hostGamepadID);
            }
           
        }
    }
}
