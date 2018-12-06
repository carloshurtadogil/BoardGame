using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameMaster : NetworkBehaviour {

    [SyncVar]
    private bool ready;
    [SyncVar]
    private bool playersmoving = true;
    private GameObject[] players;
    [SyncVar (hook = "MovePlayers")]
    private int player;

    void Start()
    {
        player = 0;
    }

    void Update()
    {
        if(!isServer) {
            return;
        }
        if (players == null) {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        if (players.Length == 2 & !ready) {
            ready = true;
            player = 1;
        }
        if(ready && playersmoving) {
            MovePlayers(player);
        }
    }

    void MovePlayers(int _p) {

        if(isLocalPlayer) {
            if (player == 1)
            {
                Debug.Log("Ready to Plays 1");
                foreach (GameObject p in players)
                {
                    if (p.GetComponent<PlayerData>().GetPlayerID() == 1 && p.GetComponent<PlayerData>().GetNetID() == netId.Value)
                    {
                        p.GetComponent<PlayerData>().Move();
                    }
                }
                player++;
                playersmoving = false;
            }
        }
    }
}
