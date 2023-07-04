using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
public class XMLManager : MonoBehaviour
{
    public static XMLManager instance { get; private set; }
    public Leaderboard leaderboard = new Leaderboard();
    private List<string> foundNames = new List<string>();
    void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Saves/"))
        {
            Debug.Log("created new directory");
            Directory.CreateDirectory(Application.dataPath + "/Saves/");
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
    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        if (File.Exists(Application.dataPath + "/Saves/leaderboard.xml"))
        {
            leaderboard.list = LoadScores();
            foreach (HighScoreEntry entry in scoresToSave)
            {
                Debug.Log("going over entry" + entry.playerName);
                for (int i = 0; i < leaderboard.list.Count; i++)
                {
                    Debug.Log("Comparing " + entry.playerName + "to " + leaderboard.list[i].playerName);
                    if (entry.playerName == leaderboard.list[i].playerName)
                    {
                        leaderboard.list[i].score += entry.score;
                        foundNames.Add(entry.playerName);
                    }
                    else if (ReturnAmount(entry.playerName, leaderboard.list) == 0)
                    {
                        leaderboard.list.Add(entry);
                    }
                   
                }
                
            }
            UpdateList();
            Debug.Log("Updated List");
        }
        else
        {
            leaderboard.list = scoresToSave;
            UpdateList();
            Debug.Log("Created List");
        }
    }

    int ReturnAmount(string name, List<HighScoreEntry> nameList)
    {
        int amountName = 0;
        for (int i = 0 ; i < nameList.Count ; i++)
        {
            if (name == nameList[i].playerName)
            {
                amountName++;
            }
        }
        return amountName;
    }
    public void UpdateList()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.dataPath + "/Saves/leaderboard.xml", FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<HighScoreEntry> LoadScores()
    {
        if (File.Exists(Application.dataPath + "/Saves/highscores.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(Application.dataPath + "/Saves/leaderboard.xml", FileMode.Open);
            leaderboard = serializer.Deserialize(stream) as Leaderboard;
            stream.Close();
        }
        return leaderboard.list;
    }

    public void clearSaveFile()
    {
        HighScoreEntry entry = new HighScoreEntry();
        entry.playerName = "empty";
        entry.playerIcon = 1;
        entry.score = 0;
        List<HighScoreEntry> list = new List<HighScoreEntry>(5);
        for (int i = 0 ; i < list.Capacity ; i++)
        {
            list.Add(entry);
        }
        leaderboard.list = list;
        UpdateList();
    }
}
[System.Serializable]
public class Leaderboard
{
    public List<HighScoreEntry> list = new List<HighScoreEntry>();
}