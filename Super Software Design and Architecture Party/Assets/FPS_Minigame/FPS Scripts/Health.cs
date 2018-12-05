using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    [SerializeField] private Text deathsText;
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    [SyncVar(hook = "UpdateDeathText")]
    public int deaths;

    public RectTransform healthBar;

    void Start()
    {
        deaths = 0;
        deathsText.text = deaths.ToString();
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            // called on the Server, but invoked on the Clients
            RpcRespawn();
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    void UpdateDeathText(int deaths)
    {
        deathsText.text = deaths.ToString();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        deaths += 1;
        UpdateDeathText(deaths);
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}