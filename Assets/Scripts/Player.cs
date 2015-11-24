using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

	[SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
    }
}
