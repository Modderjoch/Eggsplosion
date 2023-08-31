using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AchievementManager : MonoBehaviour
{
   
    public static AchievementManager instance { get; private set; }
    void Awake()
    {
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
    [System.Serializable]
    public struct achiID
    {
        public string steamID;
        public string androidID;
    }
    [SerializeField] achiID[] achiIDs;
    public int platform;
    bool isAchiUnlocked;
    public void UnlockAchi(int _index)
    {
        isAchiUnlocked = false;
        switch (platform)
        {
            case 0: //Steam
                TestSteamAchi(achiIDs[_index].steamID);
                Debug.Log($"achi with ID: {achiIDs[_index].steamID} unlocked = { isAchiUnlocked}");
                if (!isAchiUnlocked)
                {
                    SteamUserStats.SetAchievement(achiIDs[_index].steamID);
                    SteamUserStats.StoreStats();
                    Debug.Log("achievement unlocked");
                }
                break;
            default:
                break;
        }
    }
    void TestSteamAchi(string _id)
    {
        SteamUserStats.GetAchievement(_id, out isAchiUnlocked);
    }
    /*
    public void RelockAchi(int _index)
    {
        TestSteamAchi(achiIDs[_index].steamID);
        Debug.Log($"Achi with ID: {achiIDs[_index].steamID} unlocked = {isAchiUnlocked}");
        if (isAchiUnlocked)
        {
            SteamUserStats.ClearAchievement(achiIDs[_index].steamID);
            SteamUserStats.StoreStats();
        }
    }
    */
}