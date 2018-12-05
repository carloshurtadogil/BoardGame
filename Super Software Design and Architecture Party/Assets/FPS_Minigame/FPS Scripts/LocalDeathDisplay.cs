using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LocalDeathDisplay : NetworkBehaviour
{

    [SerializeField] private Text showDeathToEnemy;

    [SyncVar(hook = "UpdateDeathText")]
    private int deathsToDisplay = 0;

    // Use this for initialization
    void Update()
    {
        int deaths = this.GetComponent<Health>().deaths;
        Debug.Log("deaths: " + deaths);
        deathsToDisplay = deaths;
    }

    void UpdateDeathText(int deathsToDisplay)
    {
        showDeathToEnemy.text = deathsToDisplay.ToString();
    }
}
