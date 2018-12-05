using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour {

    private static int spawn = 0;

    //Set the user's username for the duration of the game
    public static string UserName { get; set; }

    public static GameObject[] SpawnPos { get; set; }

    public static void IncreaseIndex() {
        spawn++;
    }
}
