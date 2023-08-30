using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectResolution : MonoBehaviour
{
    Resolution detectedResolution;
    public Vector2[] resolutions;

    private bool hasDetected = false;

    private void Awake()
    {
        if (!hasDetected && !PlayerPrefs.HasKey("ResolutionIndex")) { SetDetectedResolution(); }
    }

    private void SetDetectedResolution()
    {
        detectedResolution = Screen.currentResolution;
        Debug.Log("Current resolution is: " + detectedResolution);

        Screen.SetResolution(detectedResolution.width, detectedResolution.height, true);

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
}
