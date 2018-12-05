using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour {



    public Material[] bodySkins;
    public Material[] headSkins;
    public GameObject[] spawnPoints;

    private int playerId;

    void Start()
    {
        if(isLocalPlayer) {
            Quaternion q = Quaternion.Euler(12.0f, 0, 0);
            Camera.main.transform.position = new Vector3(0.0f, 15.0f, -20f); //this.transform.position*10 - this.transform.forward * 20 + this.transform.up *10;
            Camera.main.transform.rotation = q;//LookAt(this.transform.position*20);
            Camera.main.transform.parent = this.transform;
            //StartCoroutine("CharacterUpdate");
        }
    }

    public int ScanForPlayers() {
        return GameObject.FindGameObjectsWithTag("Player").Length;
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
            Debug.Log("PlayerId = " + playerId);
            switch (playerId)
            {
                case 1:
                    RpcSpawn(0, 0);
                    break;
                case 2:
                    RpcSpawn(2, 1);
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

    //[ClientRpc]
    public void RpcSpawn(int c, int pos)
    {
        if(isLocalPlayer) {
            Debug.Log("RpcSpawn param is " + c);
            SkinnedMeshRenderer r = gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] mats = { bodySkins[c], headSkins[c] };
            r.materials = mats;
            Vector3 spawn = spawnPoints[pos].transform.position;

            gameObject.transform.position = new Vector3(spawn.x, gameObject.transform.position.y, spawn.z);
            Debug.Log("Spawn position is supposed to be " + spawn + "\nActual position is " + gameObject.transform.position);
        }
    }
}
