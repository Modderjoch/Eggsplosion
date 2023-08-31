using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public Image icon;
    public TMP_Text playerNameText;
    public Image[] fullChickens;
    public Image[] emptyChickens;

    public ScoreBoard( int _playerScore = 0, bool _wasAlive = false, string _playerName = null, Sprite _playerSprite = null, int _rank = 0)
    {
        playerScore = _playerScore;
        wasAlive = _wasAlive;
        playerName = _playerName;
        playerIcon = _playerSprite;
        rank = _rank;
    }

    public int playerIndex { get; set; }
    public string playerName { get; set; }
    public int playerScore { get; set; }
    public bool wasAlive { get; set; }
    public Sprite playerIcon { get; set; }
    public int rank { get; set; }
    void Start()
    {
        playerNameText.text = playerName;
  
        icon.sprite = playerIcon;
        for (int i = 0; i < PlayerConfigurationManager.Instance.maxAmountOfRounds; i++)
        {
            emptyChickens[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < playerScore; i++)
        {
            fullChickens[i].gameObject.SetActive(true);
        }
        
    }
}
