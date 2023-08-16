using UnityEngine;
using Steamworks;
using System.Collections;
using System.Threading;

public class SteamLeaderBoard : MonoBehaviour
{
    private const string s_leaderboardName = "Rounds Survived";
    private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

    private static SteamLeaderboard_t s_currentLeaderboard;
    private static bool s_initialized = false;
    private static CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
    private static CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();
    private static CallResult<LeaderboardScoresDownloaded_t> m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>();

    public bool isRoundOver = true;
    public RoundOverManager RoundOverManager = null;
    public ResultsManager ResultsManager = null;
    private static int downloadedScore;

    private static bool finishedDownloading = false;
    private bool isRunningDownload = false;
    private void Awake()
    {
        
    }
    private void Start()
    {
        Init();
        //GetSteamPlayerScore(); this is now on continue button
    }

    private void Update()
    {
        if (s_initialized & !finishedDownloading &!isRunningDownload) 
        {
            DownloadSteamLeaderboardEntries();
            isRunningDownload = true;
        }
    }


    public void UpdateSteamPlayerScore()
    {     
        if (finishedDownloading)
        {
            if (isRoundOver && RoundOverManager != null)
            {
                Debug.Log(RoundOverManager.playerConfigs[0].playerInput.devices[0].deviceId);
                Debug.Log(downloadedScore + " downloaded score!");
                UpdateScore(RoundOverManager.playerConfigs[0].playerScore + downloadedScore);
            }
            else if (!isRoundOver && ResultsManager != null)
            {
                Debug.Log(ResultsManager.playerConfigs[0].playerInput.devices[0].deviceId);
                Debug.Log(downloadedScore + " downloaded score!");
                UpdateScore(ResultsManager.playerConfigs[0].playerScore + downloadedScore);
             
            }
        }
    }
    public void DownloadSteamLeaderboardEntries()
    {

        Debug.Log("downloading entry??");
        SteamAPICall_t hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, -1, 1);
        m_downloadResult.Set(hSteamAPICall, OnLeaderboardScoresDownloaded);
    }

    static private void OnLeaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool failure)
    {
        if (pCallback.m_cEntryCount == 0)
        {
            finishedDownloading = true;
            return;
        }
        for (int index = 0; index < pCallback.m_cEntryCount; index++)
        {
         
            LeaderboardEntry_t leaderboardEntry;
            int[] details = { 0, 0, 0 };
            SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, index, out leaderboardEntry, details, 3);
            UnityEngine.Debug.Log("STEAM LB ENTRY :  " + leaderboardEntry.m_nScore);
            if (leaderboardEntry.m_steamIDUser == SteamUser.GetSteamID())
            {
                Debug.Log(leaderboardEntry.m_nScore);
                downloadedScore = leaderboardEntry.m_nScore;
                finishedDownloading = true;

            }
        }
    }

    public void UpdateScore(int score)
    {
        
        if (!s_initialized)
        {
            UnityEngine.Debug.Log("Can't upload to the leaderboard because isn't loaded yet");
        }
        else
        {
            UnityEngine.Debug.Log("uploading score(" + score + ") to steam leaderboard(" + s_leaderboardName + ")");
            SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(s_currentLeaderboard, s_leaderboardMethod, score, null, 0);
            m_uploadResult.Set(hSteamAPICall, OnLeaderboardUploadResult);
        }
    }

    public void Init()
    {
        SteamAPICall_t hSteamAPICall = SteamUserStats.FindOrCreateLeaderboard(s_leaderboardName, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
        m_findResult.Set(hSteamAPICall, OnLeaderboardFindResult);
        //InitTimer();
    }

    static private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
    {
        UnityEngine.Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
        s_currentLeaderboard = pCallback.m_hSteamLeaderboard;

        s_initialized = true;
    }

    static private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
    {
        UnityEngine.Debug.Log("STEAM LEADERBOARDS: failure - " + failure + " Completed - " + pCallback.m_bSuccess + " NewScore: " + pCallback.m_nGlobalRankNew + " Score " + pCallback.m_nScore + " HasChanged - " + pCallback.m_bScoreChanged);
    }


    private static Timer timer1;
    public static void InitTimer()
    {
        timer1 = new Timer(timer1_Tick, null, 0, 1000);
    }

    private static void timer1_Tick(object state)
    {
        SteamAPI.RunCallbacks();
    }

  
}