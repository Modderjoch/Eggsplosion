using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HighScoreEntry
{
    public int score;
    public string playerName;
    public int playerIcon;
    public int rank;
    public Texture2D steamIconTexture;
}
