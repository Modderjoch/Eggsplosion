using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HighScoreEntry
{
    public int score;
    public string playerName;
    public int playerIcon;
    public int rank;

    [XmlIgnore]
    public Texture2D steamIconTexture;
}
