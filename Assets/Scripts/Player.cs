using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{

    #region Variables

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    [SerializeField]
    private bool[] wasEnabled;

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    private bool _isDead = false;

    #endregion Variables

    #region Public Methods

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;

        isDead = false;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead) return;
        currentHealth -= _amount;

        if (currentHealth <= 0)
            Die();
    }

    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    #endregion Public Methods

    #region Private Methods

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is Dead");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.Instance.matchSettings.spawnTime);
        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
    }

    #endregion Private Methods
}
