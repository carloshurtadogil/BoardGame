using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScoreboardItem : MonoBehaviour
{
    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text deathCount;

    // Use this for initialization
    public void Setup(int playerNum, int deaths)
    {
        playerName.text = "P" + playerNum;
        deathCount.text = "" + deaths;
    }
}