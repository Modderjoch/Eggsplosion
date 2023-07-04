using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private bool isTeams;

    private GameObject player;
    private PlayerConfiguration[] playerConfiguration;
    void Start()
    {
        playerConfiguration = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfiguration.Length; i++)
        {
            //REMOVED code repetition on every if statement
            player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfiguration[i]);
            player.GetComponent<PlayerStats>().spawnPos = playerSpawns[i].position;
            player.GetComponent<PlayerStats>().AssignPlayerConfig(playerConfiguration[i]);
            if (!isTeams)
            {
                player.GetComponent<Shooting>().isTeams = false;
            }
            if (isTeams)
            {
                if (playerConfiguration[i].isBlue)
                {
                    player.GetComponent<PlayerStats>().isBlue = true;
                    player.GetComponent<Shooting>().isTeams = true;
                }
                else
                {
                    player.GetComponent<PlayerStats>().isBlue = false;
                    player.GetComponent<Shooting>().isTeams = true;
                }
            }
        }
    }
}
