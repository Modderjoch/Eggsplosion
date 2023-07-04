using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManagerScript : MonoBehaviour
{
    private GameObject[] players;

    //for teams
    public bool teams = false;
    public PlayerConfiguration[] playerConfigs;
    public TextMeshProUGUI casualTimerText;

    private int amountOfPlayers;
    public float timer = 10f;
    [SerializeField]
    private int redPlayerCount = 0;
    [SerializeField]
    private int bluePlayerCount = 0;
    [SerializeField]
    private bool scored = false;
    [SerializeField]
    public bool isCompetitive = false;
    [SerializeField]
    public bool isCasual = false;
    [SerializeField]
    private float casualLevelTime = 1200f;

    public string levelName;
    bool foundPlayers = false;
    private void Awake()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        CheckForRedAndBluePlayerAmount();
    }
    public int GetAmountOfPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(players.Length);
        amountOfPlayers = players.Length;
        return players.Length;
    }

    public void CheckForRedAndBluePlayerAmount()
    {
        bluePlayerCount = 0;
        redPlayerCount = 0;
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (playerConfigs[i].isBlue && playerConfigs[i].isAlive)
            {
                //Debug.Log("Player " + playerConfigs[i].playerName + "is blue? " + playerConfigs[i].isBlue);
                bluePlayerCount++;
            }
            else if (!playerConfigs[i].isBlue && playerConfigs[i].isAlive)
            {
                //Debug.Log("Player " + playerConfigs[i].playerName + "is blue? " + playerConfigs[i].isBlue);
                redPlayerCount++;
            }
        }
        // Debug.Log(redPlayerCount);
        //Debug.Log(bluePlayerCount);
    }
    //Created this method for changing the local private variable amountOfPlayers instead of having a public variable
    public void UpdateAmountOfPlayers(int minus)
    {
        amountOfPlayers -= minus;
        bluePlayerCount = 0;
        redPlayerCount = 0;
        CheckForRedAndBluePlayerAmount();

    }

    private void Update()
    {
        if (casualTimerText!= null)
        {
            casualTimerText.text = casualLevelTime.ToString();

        }
        CheckForRedAndBluePlayerAmount();
        if (!foundPlayers)
        {
            //optimizied performance a bit by invoking this function only once at the start of the scene (only time nessicary and then it gets updated by the players later
            Invoke("GetAmountOfPlayers", 0.5f);
            foundPlayers = true;
        }
        if (isCasual)
        {
            casualLevelTime -= Time.deltaTime;
            if (casualLevelTime <= 0)
            {
                SceneManager.LoadScene(levelName);
            }
        }
        if (isCompetitive)
        {
            if (amountOfPlayers == 0 || players == null)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SceneManager.LoadScene(levelName);
                }
            }
            if (!teams) // if competitive free for all
            {
                if (amountOfPlayers == 1)
                {
                    //VictoryDancePlay();
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        SceneManager.LoadScene(levelName);
                    }
                }
            }
            else if (teams) // if competitive teams
            {
                if (amountOfPlayers == 1)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        for (int i = 0; i < playerConfigs.Length; i++)
                        {
                            if (playerConfigs[i].isAlive && playerConfigs[i].isBlue)
                            {
                                GiveBlueTeamPoints(1);
                            }
                            else if (playerConfigs[i].isAlive && !playerConfigs[i].isBlue)
                            {
                                GiveRedTeamPoints(1);
                            }
                        }
                        SceneManager.LoadScene(levelName);
                    }
                }
                else if (amountOfPlayers == 2)
                {
                    if (timer <= 0)
                    {
                        Debug.Log("2 players remaining");
                        if (redPlayerCount == 2)
                        {
                            GiveRedTeamPoints(1);
                            SceneManager.LoadScene(levelName);
                        }
                        else if (bluePlayerCount == 2)
                        {
                            GiveBlueTeamPoints(1);
                            SceneManager.LoadScene(levelName);
                        }
                    }
                }
            }
        }
    }

    void VictoryDancePlay()
    {
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            if (playerConfigs[i].isAlive)
            {
                players[i].GetComponent<PlayerStats>().VictoryDance(playerConfigs[i]);

            }
        }
    }

    void GiveBlueTeamPoints(int point)
    {
        if (!scored)
        {
            for (int i = 0; i < playerConfigs.Length; i++)
            {
                if (playerConfigs[i].isBlue)
                {
                    playerConfigs[i].playerScore += point;
                }
            }
            scored = true;
        }
    }

    void GiveRedTeamPoints(int point)
    {
        if (!scored)
        {
            for (int i = 0; i < playerConfigs.Length; i++)
            {
                if (!playerConfigs[i].isBlue)
                {
                    playerConfigs[i].playerScore += point;
                }
            }
            scored = true;
        }
    }
}
