using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LocalHealthbar : NetworkBehaviour {
    // Use this for initialization
    void Start () {
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        if (isLocalPlayer)
        {
            rectTransform.localPosition = new Vector3(-0.01f, 0.45f, -0.23f);
            //rt.SetPositionAndRotation(new Vector3(20, -10, -6), q);
            //this.rect.Set(0, 0, healthBar.rect.width, healthBar.rect.height);
            //healthBar.transform.parent = this.transform;
        }
    }
}


/*
 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class HealthBar : NetworkBehaviour
{
    public RectTransform healthBar;
    void Start()
    {
        RpcStart();
    }
    [ClientRpc]
    void RpcStart()
    {
        if (isLocalPlayer)
        {
            Quaternion q = Quaternion.Euler(12.0f, 0, 0);
            //healthBar.position = new Vector3(20, -10, -6);
            healthBar.sizeDelta = new Vector2(10, healthBar.sizeDelta.y);
            //RectTransform rt = this.GetComponent<RectTransform>();
            //rt.SetPositionAndRotation(new Vector3(20, -10, -6), q);
            //this.rect.Set(0, 0, healthBar.rect.width, healthBar.rect.height);
            //healthBar.transform.parent = this.transform;
        }
    }
}
Chat Conversation End
Type a message... 
 */
