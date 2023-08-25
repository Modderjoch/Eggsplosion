using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Steamworks;

public class LeaderBoardManagerSteam : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject Panel;
    public GameObject localPanel;
    private List<HighScoreEntry> highScores = new List<HighScoreEntry>();
    private List<HighScoreEntry> bestScores = new List<HighScoreEntry>();

    public List<GameObject> textObjects = new List<GameObject>();

    private int numberOfBoards;


    // 
    private SteamLeaderboard_t s_currentLeaderboard;
    private bool s_initialized = false;
    private CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
    private CallResult<LeaderboardScoresDownloaded_t> m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>();

    private static List<CallResult<LeaderboardFindResult_t>> m_findResult_list = new List<CallResult<LeaderboardFindResult_t>>();

    private Callback<AvatarImageLoaded_t> m_avatarImageLoaded;

    List<HighScoreEntry> LeaderboardDataset;

    private ulong steamdId;

    private int entries = 5;

    public bool isGlobal;
    public bool isKillBoard;
    public string leaderBoardNameSet = "";

    private void Awake()
    {
        //FindLeaderBoard(leaderBoardNameSet);
    }
    private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
    {
        Debug.Log($"Steam Leaderboard Find: Did it fail? {failure}, Found: {pCallback.m_bLeaderboardFound}, leaderboardID: {pCallback.m_hSteamLeaderboard.m_SteamLeaderboard}");
        s_currentLeaderboard = pCallback.m_hSteamLeaderboard;
        s_initialized = true;
    }
    public void GetLeaderBoardData(ELeaderboardDataRequest _type = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, int entries = 5)
    {
        Debug.Log("getting data!");
        SteamAPICall_t hSteamAPICall;
        switch (_type)
        {
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, 1, entries);
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, -(entries / 2), (entries / 2));
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, 1, entries);
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
        }
        //Note that the LeaderboardDataset will not be updated immediatly (see callback below)
    }

    private void OnLeaderboardDownloadResult(LeaderboardScoresDownloaded_t pCallback, bool failure)
    {
        Debug.Log($"Steam Leaderboard Download: Did it fail? {failure}, Result - {pCallback.m_hSteamLeaderboardEntries}");
        LeaderboardDataset = new List<HighScoreEntry>();
        //Iterates through each entry gathered in leaderboard
        for (int i = 0; i < pCallback.m_cEntryCount; i++)
        {

            Debug.Log("checking entries");
            LeaderboardEntry_t leaderboardEntry;
            SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out leaderboardEntry, null, 0);
            //Example of how leaderboardEntry might be held/used
            HighScoreEntry lD = new HighScoreEntry();
            lD.playerName = SteamFriends.GetFriendPersonaName(leaderboardEntry.m_steamIDUser);
            lD.rank = leaderboardEntry.m_nGlobalRank;
            lD.score = leaderboardEntry.m_nScore;

            int imgId = SteamFriends.GetLargeFriendAvatar(leaderboardEntry.m_steamIDUser);
            //if (imgId == -1) { Debug.Log("no img"); return; }
            lD.steamIconTexture = GetSteamImageAsTexture(imgId);
            //img.texture = GetSteamImageAsTexture(imgId);
            
          
            Debug.Log($"User: {lD.playerName} - Score: {lD.score} - Rank: {lD.rank}");
            LeaderboardDataset.Add( lD );

            SortSteamScores();
            InitiateLeaderBoard();
        }
        
    }

    private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
    {
        if(callback.m_steamID.m_SteamID != steamdId) { return; }
        
    }
    private Texture2D GetSteamImageAsTexture(int Image)
    {
        Texture2D text = null;
        bool isValid = SteamUtils.GetImageSize(Image, out uint width, out uint height);

        if (isValid)
        {
            Debug.Log("image is valid");
            byte[] _image = new byte[width * height * 4];
            isValid = SteamUtils.GetImageRGBA(Image, _image, (int)(width * height * 4));
            if (isValid)
            {
                Debug.Log("image is valid again!");
                text = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                text.LoadRawTextureData(_image);
                text.Apply();

            }
        }
        if (text == null)
        {
            Debug.Log("texture is null");
        }
        return text;
    }
    private void Start()
    {
        if (!isGlobal)
        {
            StartBoardFriends(leaderBoardNameSet);
            Invoke("MakeBoard", 1f);
        }
        else if (isGlobal)
        {
            StartBoard(leaderBoardNameSet);
            Invoke("MakeBoard", 1f);
        }
    }

    public void SetLeaderBoardName(string nameToSet)
    {
        leaderBoardNameSet = nameToSet;
    }

    public void StartBoard(string leaderboardName)
    {
        FindLeaderBoard(leaderboardName);

        GetLeaderBoardData(ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, entries);
    }

    public void ClearLeaderboard()
    {
        while (Panel.transform.childCount > 0) { DestroyImmediate(Panel.transform.GetChild(0).gameObject); }
    }

    public void StartBoardFriends(string leaderboardName)
    {

        FindLeaderBoard(leaderboardName);

        GetLeaderBoardData(ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends, entries);
    }

    public void FindLeaderBoard(string leaderboardName)
    {
        SteamAPICall_t hsteamAPICall = SteamUserStats.FindLeaderboard(leaderboardName);
        CallResult<LeaderboardFindResult_t> findResult = new CallResult<LeaderboardFindResult_t>();
        findResult.Set(hsteamAPICall, OnLeaderboardFindResult);

        m_findResult_list.Add(findResult);
    }
    public void MakeBoard()
    {
        //SortSteamScores();
        InitiateLeaderBoard();
    }

    public void SortSteamScores()
    {
        Debug.Log("sorting Scores");
        bestScores = LeaderboardDataset.OrderByDescending(s => s.score).Take(entries).ToList();
        Debug.Log(bestScores.Count);
    }

    public void InitiateLeaderBoard()
    {
        Debug.Log(bestScores.Count);
        for (int i = 0; i < bestScores.Count; i++)
        {
            Debug.Log("CheckingScores");
            
            AddScoreBoard();
            UpdateScoreBoard(bestScores[i].score, i, bestScores[i].playerName, bestScores[i].steamIconTexture);
        }
    }

    public void ClearExisting()
    {
        while (Panel.transform.childCount > 0) { DestroyImmediate(Panel.transform.GetChild(0).gameObject); }
        while (localPanel.transform.childCount > 0) { DestroyImmediate(localPanel.transform.GetChild(0).gameObject); }
        m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>();
        m_findResult_list.Clear();
        textObjects.Clear();
        bestScores.Clear();
        numberOfBoards = 0;
    }


    public void AddScoreBoard()
    {
        if (numberOfBoards <= 4)
        {
            Debug.Log("Added");
            var board = Instantiate(scoreText, new Vector3(Panel.transform.position.x, Panel.transform.position.y - (7 * (Screen.height / 100) * numberOfBoards), Panel.transform.position.z), Panel.transform.rotation, Panel.transform);
            textObjects.Add(board);
            numberOfBoards++;
        }
    }
    public void UpdateScoreBoard(int score, int boardInstance, string playerName, Texture2D Img)
    {
        Debug.Log("Updated");
        textObjects[boardInstance].GetComponent<LeaderBoardPanel>().playerName = playerName;
        textObjects[boardInstance].GetComponent<LeaderBoardPanel>().playerScore = score;  
        textObjects[boardInstance].GetComponent<LeaderBoardPanel>().isSteam = true;
        textObjects[boardInstance].GetComponent<LeaderBoardPanel>().isKillBoard = isKillBoard;
        textObjects[boardInstance].GetComponent<LeaderBoardPanel>().steamIcon.texture = Img;

    }

    public void ClearLeaderBoard()
    {
        foreach (var board in textObjects)
        {
            Destroy(board.gameObject);
        }
        highScores.Clear();
        bestScores.Clear();
        textObjects.Clear();
        numberOfBoards = 0;     
    }

    public void UpdateLists()
    {
        highScores = XMLManager.instance.LoadScores();
        bestScores = highScores.OrderByDescending(s => s.score).Take(5).ToList();
    }

    public void clearLeaderBoardAndSaveFile()
    {
        //    List<HighScoreEntry> list = new List<HighScoreEntry>();
        //    clearLeaderBoard();

        ClearLeaderBoard();
        XMLManager.instance.ClearSaveFile();
        InitiateLeaderBoard();
    }
}
