using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public static int playerIndex = 1;  

    [SerializeField]
    private GameObject sceneCamera;

    /*
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
        }
        else
        {
            instance = this;
        }
    }
    */
    public void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera == null)
            return;

        sceneCamera.SetActive(isActive);
    }

    #region Health tracking

    private const string PLAYER_ID_PREFIX = "Player ";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netID, Player p)
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, p);
        p.playerNum = playerIndex;
        playerIndex += 1;
    }

    /*
    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }
    */

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    #endregion
    //void OnGUI ()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}
}
