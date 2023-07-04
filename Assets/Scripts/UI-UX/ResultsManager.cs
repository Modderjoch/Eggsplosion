using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    PlayerConfiguration[] playerConfigs;
    public GameObject canvas;
    public GameObject prefab;
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
                PlayerConfigurationManager.Instance.SetHighScoreEntry(i, playerConfigs[i].playerScore, playerConfigs[i].playerName, playerConfigs[i].spriteId);
                AddScoreBoard();
                UpdateScoreBoard(playerConfigs[i].playerScore, i, playerConfigs[i].playerIndex, true, playerConfigs[i].playerName, playerConfigs[i].playerSprite);
                Debug.Log("Game Over");
                
            }
            else if (!playerConfigs[i].isAlive)
            {
                PlayerConfigurationManager.Instance.SetHighScoreEntry(i, playerConfigs[i].playerScore, playerConfigs[i].playerName, playerConfigs[i].spriteId);
                Debug.Log("Added Score for player " + playerConfigs[i].playerIndex);
                AddScoreBoard();
                UpdateScoreBoard(playerConfigs[i].playerScore, i, playerConfigs[i].playerIndex, false, playerConfigs[i].playerName, playerConfigs[i].playerSprite);
            }
            numberOfPlayers++;
        }
        highScores = PlayerConfigurationManager.Instance.GetPlayerHighScores();
        Save();
    }
    void Save()
    {
        XMLManager.instance.SaveScores(highScores);
    }
    public void AddScoreBoard()
    {
        var board = Instantiate(prefab, new Vector3(canvas.transform.position.x, canvas.transform.position.y - ((canvas.transform.position.y/4 -35) * numberOfPlayers), canvas.transform.position.z), canvas.transform.rotation, canvas.transform);
        scores.Add(board);
    }
    public void UpdateScoreBoard(int score, int boardInstance, int playerIndex, bool wasAlive, string playerName, Sprite playerIcon)
    {

        scores[boardInstance].GetComponent<ScoreBoard>().playerName = playerName;
        scores[boardInstance].GetComponent<ScoreBoard>().playerScore = score;
        scores[boardInstance].GetComponent<ScoreBoard>().playerIndex = playerIndex;
        scores[boardInstance].GetComponent<ScoreBoard>().wasAlive = wasAlive;
        scores[boardInstance].GetComponent<ScoreBoard>().playerIcon = playerIcon;
    }
    public void ToResultScreen()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void MainMenu()
    {
        GameObject configManager = GameObject.FindGameObjectWithTag("GameController");
        Destroy(configManager);
        SceneManager.LoadScene("MainMenu");
    }
}
