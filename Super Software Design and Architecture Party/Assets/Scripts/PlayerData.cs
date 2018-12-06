using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour {



    public Material[] bodySkins;
    public Material[] headSkins;
    public GameObject[] spawnPoints;
    public CardGenerator cg;


    private bool startScanning;
    [SyncVar]
    private bool tts;
    private bool isTurn;
    private int newScan;
    private int playerId;
    private int playersInRoom;
    private NetworkInstanceId id;


    void Start()
    {
        if(isLocalPlayer) {
            Quaternion q = Quaternion.Euler(12.0f, 0, 0);
            Camera.main.transform.position = new Vector3(0.0f, 15.0f, -20f); //this.transform.position*10 - this.transform.forward * 20 + this.transform.up *10;
            Camera.main.transform.rotation = q;//LookAt(this.transform.position*20);
            Camera.main.transform.parent = this.transform;
            id = netId;
            Debug.Log("ID: " + id.Value);
            //StartCoroutine("CharacterUpdate");
        }
    }

    void Update()
    {
        tts = true;
        if (isLocalPlayer) {
            if(Input.GetKeyDown("p")) {
                Move();
            }
            if (startScanning)
            {
                newScan = ScanForPlayers();
                if (newScan != playersInRoom)
                {
                    playersInRoom = newScan;

                }
            }
        }


    }

    public void Move() {
        FollowPath p = GetComponent<FollowPath>();
        p.Draw();
    }

    public int ScanForPlayers() {
        return GameObject.FindGameObjectsWithTag("Player").Length;
    }

    public uint GetNetID() {
        return id.Value;
    }

    public override void OnStartLocalPlayer()
    {
        Debug.Log("Reached Local");
        StartCoroutine("CharacterUpdate");


    }

    public IEnumerator CharacterUpdate()
    {
        yield return new WaitForSeconds(3);
        if (isLocalPlayer)
        {
            Debug.Log("This should appear");
            playerId = ScanForPlayers();
            playersInRoom = playerId;
            Debug.Log("PlayerId = " + playerId);
            switch (playerId)
            {
                case 1:
                    RpcSpawn(0, 1);
                    break;
                case 2:
                    RpcSpawn(3, 2);

                    break;
                case 3:
                    RpcSpawn(2, 2);
                    break;
                case 4:
                    RpcSpawn(3, 3);
                    break;

                default:
                    Debug.Log("CharacterUpdate() Coroutine exceeded maximum capacity");
                    break;
            }
        }
        else {
            Debug.Log("This should not appear\n Local Failed");
        }
    }

    public int GetPlayerID() {
        return playerId;
    }

    //[ClientRpc]
    public void RpcSpawn(int c, int pos)
    {
        tts = true;
        Debug.Log("RpcSpawn param is " + c);
        SkinnedMeshRenderer r = gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = { bodySkins[c], headSkins[c] };
        r.materials = mats;
        Vector3 spawn = spawnPoints[pos].transform.position;

        gameObject.transform.position = new Vector3(spawn.x, gameObject.transform.position.y, spawn.z);
        startScanning = true;
    }

    public void ReskinOthersInLocal() {
        GameObject[] playersInRoomObjects = GameObject.FindGameObjectsWithTag("Players");
        SortPlayers(playersInRoomObjects);
        for (int i = 0; i < playersInRoomObjects.Length; i++) { 
            if(playersInRoomObjects[i].GetComponent<PlayerData>().playerId != this.playerId) {
                Material[] m = { bodySkins[i], headSkins[i] };
                playersInRoomObjects[i].gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().materials = m;
            }
        }
    }

    public void SortPlayers(GameObject[] p) {
        int sid, smallest, current;
        GameObject temp;
        Debug.Log("Length of list: " + p.Length);
        for(int i = 0; i < p.Length; i++) {
            sid = i; 
            smallest = p[i].GetComponent<PlayerData>().GetPlayerID();
            for (int j = i; j < p.Length; j++) {
                current = p[j].GetComponent<PlayerData>().GetPlayerID();
                if ( current < smallest ) {
                    smallest = current;
                    sid = j;
                }
            }
            temp = p[sid];
            p[sid] = p[i];
            p[i] = temp;
        }
        foreach(GameObject pl in p) {
            Debug.Log("PID: " + pl.GetComponent<PlayerData>().playerId);
        }
    }
}
