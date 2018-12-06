using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
    
    [SerializeField]
    GameObject scoreboardItem;

    [SerializeField]
    Transform scoreboardPlayerList;

    private static bool done;

    void Start()
    {
        done = false;
    }
	
	void OnEnable()
    {
        if(done)
        {
            return;
        }
        // Get an array of players
        Player[] players = PlayerManager.GetAllPlayers();

        foreach ( Player player in players)
        {
            Debug.Log(player.playerNum + " | " + player.deaths);

            GameObject itemGO = Instantiate(scoreboardItem, scoreboardPlayerList);
            Debug.Log("otemGO: " + itemGO);

            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            Debug.Log(item);
            if(item != null)
            {
                item.Setup(player.playerNum, player.deaths);
                done = true;
            }
        }
	}

    void OnDisable()
    {
        foreach (Transform child in scoreboardPlayerList)
        {
            Destroy(child.gameObject);
        }
    }
   
}
