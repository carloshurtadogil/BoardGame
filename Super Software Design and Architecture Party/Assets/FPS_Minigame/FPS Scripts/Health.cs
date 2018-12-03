using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
    [SerializeField] private Text lifeText;

    public const int maxHealth = 100;
    public const int maxLives = 20;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    [SyncVar(hook = "LoseLife")]
    private int lives;

    public RectTransform healthBar;

    void Start()
    {
        lives = maxLives;
        lifeText.text = lives.ToString();
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

    void LoseLife(int lives)
    {
        lifeText.text = lives.ToString();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        lives -= 1;
        LoseLife(lives);
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}