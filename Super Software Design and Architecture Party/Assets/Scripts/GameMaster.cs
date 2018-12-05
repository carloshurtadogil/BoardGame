using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameMaster : NetworkBehaviour {

    private GameObject[] players;
    private int spawnIndex;
    public GameObject[] spawnPoints;
    public GameObject[] characters;

    
    // Use this for initialization
    void Start () {
        if(!isServer) {
            return;
        }

        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("There are " + players.Length + " players on the field");
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    void Draw() {

        if(players.Length > 0) {
            players[0].GetComponent<FollowPath>().Draw();
        }
    }
}
