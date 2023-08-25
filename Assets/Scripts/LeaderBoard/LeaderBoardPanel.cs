using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderBoardPanel : MonoBehaviour
{
    public Image[] iconsToChooseFrom;
    public Image icon;
    public TMP_Text playerNameText;
    public int spriteId;
    //
    public RawImage steamIcon;
    public bool isSteam = false;
    public bool isKillBoard = false;
    public LeaderBoardPanel(int _playerIndex, int _playerScore, bool _wasAlive, string _playerName, int _spriteId)
    {
        playerIndex = _playerIndex;
        playerScore = _playerScore;
        wasAlive = _wasAlive;
        playerName = _playerName;
        spriteId = _spriteId;
    }

    public int playerIndex { get; set; }
    public string playerName { get; set; }
    public int playerScore { get; set; }
    public bool wasAlive { get; set; }
    public Sprite playerIcon { get; set; }


    //

    void Start()
    {
        if (!isSteam)
        {
            icon.sprite = iconsToChooseFrom[spriteId].sprite;
            playerNameText.text = playerName + " Won " + playerScore + " rounds!";
        }
        if (isSteam && !isKillBoard) 
        {
            Debug.Log("is steam entry and round");
            steamIcon.gameObject.SetActive(true);
            playerNameText.text = playerName + " Won " + playerScore + " rounds!";
        }
        else if (isSteam && isKillBoard)
        {
            Debug.Log("is steam entry and kill");
            steamIcon.gameObject.SetActive(true);
            playerNameText.text = playerName + " Killed " + playerScore + " chickens! ";
        }
    }
}
