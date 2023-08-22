using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Options : MonoBehaviour
{
    Resolution detectedResolution;
    public Vector2[] resolutions;
    public string[] screenModes;
    public bool setRes = true;
    private int resolutionIndex;
    private int screenModeIndex;
    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI screenmodeText;

    private bool hasDetected = false;

    private void Start()
    {
        if (setRes)
        {
            resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 2);
            screenModeIndex = PlayerPrefs.GetInt("ScreenmodeIndex", 2);
            SetResolution(resolutionIndex, screenModeIndex);
            setRes = false;
        }

        if (!hasDetected && !PlayerPrefs.HasKey("ResolutionIndex")) { SetDetectedResolution(); }
        if (resolutionText != null) { resolutionText.text = resolutions[resolutionIndex].x + "x" + resolutions[resolutionIndex].y; }
        if (screenmodeText != null) { screenmodeText.text = screenModes[screenModeIndex]; }
    }

    private void SetResolution(int index, int modeIndex)
    {
        int width, height;

        width = Mathf.RoundToInt(resolutions[index].x);
        height = Mathf.RoundToInt(resolutions[index].y);

        Screen.SetResolution(width, height, (FullScreenMode)modeIndex);

        Debug.Log(Screen.currentResolution);
        if (resolutionText != null)
        {
            resolutionText.text = width + "x" + height;
        }

        PlayerPrefs.Save();
    }

    public void IncreaseResolution()
    {
        resolutionIndex = (resolutionIndex + 1) % (resolutions.Length);
        Debug.Log("Increased index: " + resolutionIndex);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        SetResolution(resolutionIndex, screenModeIndex);
    }

    public void DecreaseResolution()
    {
        resolutionIndex = (resolutionIndex - 1 + (resolutions.Length)) % (resolutions.Length);
        Debug.Log("Decreased index: " + resolutionIndex);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        SetResolution(resolutionIndex, screenModeIndex);
    }

    public void SwitchScreenMode(int mode)
    {
        screenModeIndex = mode;
        screenmodeText.text = screenModes[mode];

        PlayerPrefs.SetInt("ScreenmodeIndex", mode);
        SetResolution(resolutionIndex, mode);
    }

    private void SetDetectedResolution()
    {
        detectedResolution = Screen.currentResolution;
        Debug.Log("Current resolution is: " + detectedResolution);

        Screen.SetResolution(detectedResolution.width, detectedResolution.height, (FullScreenMode)screenModeIndex);

        Vector2 detectedRes = new Vector2(detectedResolution.width, detectedResolution.height);
        CheckResolution(detectedRes);

        hasDetected = true;
    }

    private void CheckResolution(Vector2 detectedResolution)
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            Vector2 resolution = resolutions[i];
            if (detectedResolution == resolution)
            {
                Debug.Log("Detected resolution matches preset: " + resolution);
                Debug.Log("Index of matched preset: " + i);
                PlayerPrefs.SetInt("ResolutionIndex", i);
                // Do something specific for this resolution
                break;
            }
        }
    }

    public void NextMode()
    {
        screenModeIndex = (screenModeIndex + 1) % (screenModes.Length);
        if (screenModeIndex == 1)
        {
            screenModeIndex++;
        }

        Debug.Log("Increased index: " + screenModeIndex);
        SwitchScreenMode(screenModeIndex);
    }


    public void LastMode()
    {
        screenModeIndex = (screenModeIndex - 1 + screenModes.Length) % (screenModes.Length);
        if (screenModeIndex == 1)
        {
            screenModeIndex--;
        }

        Debug.Log("Decreased index: " + screenModeIndex);

        SwitchScreenMode(screenModeIndex);
    }
}
