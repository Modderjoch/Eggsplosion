using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

public class RoundOverManager : MonoBehaviour
{
    public bool isCasual = false;
    public PlayerConfiguration[] playerConfigs;
    public GameObject panel;
    public GameObject prefab;
    public GameObject confirmationButton;
    public GameObject continueButton;

    public string[] sceneName;

    bool panelIsActive = false;
    List<HighScoreEntry> highScores;
    private int numberOfPlayers = 0;
    private List<GameObject> scores = new List<GameObject>();
    void Awake()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (playerConfigs[i].isAlive)
            {  
                AddScoreBoard();
                UpdateScoreBoard(playerConfigs[i].playerScore, i, playerConfigs[i].playerIndex, true, playerConfigs[i].playerName, playerConfigs[i].playerSprite, playerConfigs[i].killAmount);
                Debug.Log("Round Over");
                PlayerConfigurationManager.Instance.SetHighScoreEntry(i, playerConfigs[i].playerScore, playerConfigs[i].playerName, playerConfigs[i].spriteId);
                if (playerConfigs[i].playerScore >= PlayerConfigurationManager.Instance.maxAmountOfRounds)
                {
                    Debug.Log("Going Result Screen!");
                    ToResultScreen();
                }
            }
            else if (!playerConfigs[i].isAlive)
            {
                PlayerConfigurationManager.Instance.SetHighScoreEntry(i, playerConfigs[i].playerScore, playerConfigs[i].playerName, playerConfigs[i].spriteId);
                Debug.Log("Added Score for player " + playerConfigs[i].playerName);
                AddScoreBoard();
                UpdateScoreBoard(playerConfigs[i].playerScore, i, playerConfigs[i].playerIndex, false, playerConfigs[i].playerName, playerConfigs[i].playerSprite, playerConfigs[i].killAmount);
            }
            numberOfPlayers++;
        }
 

    }

    public void SaveAndQuit()
    {
        highScores = PlayerConfigurationManager.Instance.GetPlayerHighScores();
        AchievementTracker.instance.SaveScores(playerConfigs[0].killAmount, playerConfigs[0].playerScore);
        XMLManager.instance.SaveScores(highScores);
    }
    public void AddScoreBoard()
    {      
        var board = Instantiate(prefab,new Vector3(panel.transform.position.x, panel.transform.position.y - ((panel.transform.position.y/4 - 50f) * numberOfPlayers), panel.transform.position.z), panel.transform.rotation, panel.transform);
        scores.Add(board);
    }
    public string LoadRandomLevel()
    {
        int random;
        random = Random.Range(0, sceneName.Length); 
        return sceneName[random];
    }

    public void UpdateScoreBoard(int score, int boardInstance, int playerIndex, bool wasAlive, string playerName, Sprite playerIcon, int kills)
    {

        scores[boardInstance].GetComponent<ScoreBoard>().playerName = playerName;
        scores[boardInstance].GetComponent<ScoreBoard>().playerScore = score;
        scores[boardInstance].GetComponent<ScoreBoard>().playerIndex = playerIndex;
        scores[boardInstance].GetComponent<ScoreBoard>().wasAlive = wasAlive;
        scores[boardInstance].GetComponent<ScoreBoard>().playerIcon = playerIcon;
        scores[boardInstance].GetComponent<ScoreBoard>().kills = kills;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(LoadRandomLevel());
    }

    public void QuitConfirmation()
    {
        if (panelIsActive)
        {
            continueButton.GetComponent<Button>().Select();
            panelIsActive = false;
        }
        else
        {
            confirmationButton.GetComponent<Button>().Select();
            panelIsActive = true;
        }
        
    }

    public void ToResultScreen()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void MainMenu()
    {
        GameObject configManager = GameObject.FindGameObjectWithTag("GameController");
        GameObject casualManager = GameObject.FindGameObjectWithTag("CasualInfo");
        Destroy(configManager);
        Destroy(casualManager);
        AudioManager.Instance.StopAllMusic();

        SceneManager.LoadScene("MainMenu");
    }
}
