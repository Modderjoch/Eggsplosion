using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private float currentRes = 0;
    private bool fullscreen = true;
    private bool windowed = false;
    private bool borderless = false;

    public void SetRes640x480()
    {
        Debug.Log("SetRes 640x480");
        if (fullscreen)
        {
            Screen.SetResolution(640, 480, true);
            currentRes = 2;
        }

        if (windowed)
        {
            Screen.SetResolution(640, 480, false);
            currentRes = 2;
        }

        if (borderless)
        {
            Screen.SetResolution(640, 480, FullScreenMode.ExclusiveFullScreen);
            currentRes = 2;
        }
    }

    public void SetRes1366x768()
    {
        Debug.Log("SetRes 1366x768");
        if (fullscreen)
        {
            Screen.SetResolution(1366, 768, true);
            currentRes = 1;
        }

        if (windowed)
        {
            Screen.SetResolution(1366, 768, false);
            currentRes = 1;
        }

        if (borderless)
        {
            Screen.SetResolution(1366, 768, FullScreenMode.ExclusiveFullScreen);
            currentRes = 1;
        }
    }

    public void SetRes1920x1080()
    {

        Debug.Log("SetRes 1920x1080");
        if (fullscreen)
        {
            Screen.SetResolution(1920, 1080, true);
            currentRes = 0;
        }

        if (windowed)
        {
            Screen.SetResolution(1920, 1080, false);
            currentRes = 0;
        }

        if (borderless)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
            currentRes = 0;
        }
    }

    public void Windowed()
    {
        if (currentRes == 0)
        {
            Screen.SetResolution(1920, 1080, false);
            windowed = true;
            fullscreen = false;
            borderless = false;
            Debug.Log("SetRes 1920x1080, windowed");
        }
        else
        {
            if (currentRes == 1)
            {
                Screen.SetResolution(1366, 768, false);
                windowed = true;
                fullscreen = false;
                borderless = false;
                Debug.Log("SetRes 1366x768, windowed");
            }
            else
            {
                if (currentRes == 2)
                {
                    Screen.SetResolution(640, 480, false);
                    windowed = true;
                    fullscreen = false;
                    borderless = false;
                    Debug.Log("SetRes 640x480, windowed");
                }
            }
        }
    }

    public void Borderless()
    {
        if (currentRes == 0)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
            borderless = true;
            fullscreen = false;
            windowed = false;
            Debug.Log("SetRes 1920x1080, borderless");
        }
        else
        {
            if (currentRes == 1)
            {
                Screen.SetResolution(1366, 768, FullScreenMode.ExclusiveFullScreen);
                borderless = true;
                fullscreen = false;
                windowed = false;
                Debug.Log("SetRes 1366x768, borderless");
            }
            else
            {
                if (currentRes == 2)
                {
                    Screen.SetResolution(640, 480, FullScreenMode.ExclusiveFullScreen);
                    borderless = true;
                    fullscreen = false;
                    windowed = false;
                    Debug.Log("SetRes 640x480, borderless");
                }
            }
        }
    }

    public void Fullscreen()
    {
        if (currentRes == 0)
        {
            Screen.SetResolution(1920, 1080, true);
            fullscreen = true;
            borderless = false;
            windowed = false;
            Debug.Log("SetRes 1920x1080, fullscreen");
        }
        else
        {
            if (currentRes == 1)
            {
                Screen.SetResolution(1366, 768, true);
                fullscreen = true;
                borderless = false;
                windowed = false;
                Debug.Log("SetRes 1366x768, fullscreen");
            }
            else
            {
                if (currentRes == 2)
                {
                    Screen.SetResolution(640, 480, true);
                    fullscreen = true;
                    borderless = false;
                    windowed = false;
                    Debug.Log("SetRes 640x480, fullscreen");
                }
            }
        }
    }
}
