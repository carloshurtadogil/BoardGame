using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameMaster : NetworkBehaviour {

    private GameObject[] players;
    private int currentAmount;
    private int newAmount;
    private int spawnIndex;
    public Button draw;
    public GameObject[] spawnPoints;
    public GameObject[] characters;

    void Awake()
    {
        DataManager.SpawnPos = spawnPoints;

    }
    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("There are " + players.Length + " players on the field");
        draw.onClick.AddListener(Draw);
        currentAmount = 0;
        newAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(players.Length == 0) {
            players = GameObject.FindGameObjectsWithTag("Player");
            float count = 0.0f;

            //Debug.Log("There are " + players.Length + " players on the field");
        }
        newAmount = NetworkServer.connections.Count;

        if(newAmount != currentAmount) {
            currentAmount = newAmount;
            players[0].transform.position = spawnPoints[0].transform.position;
            Debug.Log("There are " + newAmount + " connections");
            //RpcSpawn();
            //CmdSpawn();
        }
    }

    void Draw() {

        if(players.Length > 0) {
            players[0].GetComponent<FollowPath>().Draw();
        }
    }



    [ClientRpc]
    void RpcSpawn() {
        if(isLocalPlayer) {
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0) {
                spawnPoint = spawnPoints[spawnIndex].transform.position;
            }
            players[spawnIndex].gameObject.transform.position = spawnPoint;
            spawnIndex++;
        }
    }

    [Command]
    public void CmdSpawn()
    {
        GameObject go = Instantiate(characters[spawnIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
        Debug.Log("Name: " + go.name + "\nPosition: " + go.transform.position );
       //Debug.Log("Connection to Client: "+ NetworkServer.connections[0].co );
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
