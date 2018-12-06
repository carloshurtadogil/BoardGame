using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    private NetworkIdentity netID;
    public int playerNum = 0;
    public int deaths = 0;
    
    void Start()
    {
        netID = this.GetComponent<NetworkIdentity>();
        PlayerManager.RegisterPlayer(netID.ToString(), this);
    }
	// Update is called once per frame
	void Update () {
        deaths = this.GetComponent<Health>().deaths;
    }
}
