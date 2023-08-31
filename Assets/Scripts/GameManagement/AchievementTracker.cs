using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
public class AchievementTracker : MonoBehaviour
{
    public static AchievementTracker instance { get; private set; }
    public AchievementsData achiData = new AchievementsData();

    void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Achi/"))
        {
            Debug.Log("created new directory");
            Directory.CreateDirectory(Application.dataPath + "/Achi/");
        }
        if (instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

    }
    public void SaveScores(int KillsToAdd, int RoundsToAdd)
    {
        if (File.Exists(Application.dataPath + "/Achi/scoretracking.xml"))
        {
            //LOAD STORED DATA
            achiData = LoadScores();
            Debug.Log("previous achi scores" + achiData.accumilatedScore + " - " +achiData.accumilatedKills);
            //Adding the score obtained in roundOver
            achiData.accumilatedKills += KillsToAdd;
            achiData.accumilatedScore += RoundsToAdd;
            UpdateList();
            Debug.Log("Updated AchievementTracker");
            if (achiData.accumilatedScore >= 100)
            {
                AchievementManager.instance.UnlockAchi(4);
            }
        }
        else
        {
            achiData.accumilatedScore = 0;
            achiData.accumilatedKills = 0;

            achiData.accumilatedKills += KillsToAdd;
            achiData.accumilatedScore += RoundsToAdd;

            UpdateList();
            Debug.Log("Created Achievement Tracker");
        }
    }
    public void UpdateList()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AchievementsData));
        FileStream stream = new FileStream(Application.dataPath + "/Achi/scoretracking.xml", FileMode.Create);
        serializer.Serialize(stream, achiData);
        stream.Close();
    }

    public AchievementsData LoadScores()
    {
        if (File.Exists(Application.dataPath + "/Achi/scoretracking.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AchievementsData));
            FileStream stream = new FileStream(Application.dataPath + "/Achi/scoretracking.xml", FileMode.Open);
            achiData = serializer.Deserialize(stream) as AchievementsData;
            stream.Close();
        }
        return achiData;
    }

}
[System.Serializable]
public class AchievementsData
{
    public int accumilatedScore;
    public int accumilatedKills;
}