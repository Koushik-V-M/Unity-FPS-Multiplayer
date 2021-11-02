using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 50;

    [SyncVar]
    public string username="Loading...";

    public int kills;
    public int deaths;

    [SyncVar]
    private int currentHealth;

    public float getHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.LogError(getHealthPct());
        }
    }
    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;
    private bool firstSetup = true;

    public void setup()
    {
        if (isLocalPlayer)
        {
            GetComponent<player_setup>().playerUIInstance.SetActive(true);            
            CmdBroadcastNewPlayerSetup();

        }

    }

    [Command]
    private void CmdBroadcastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }
        setDefaults();
    }

    [ClientRpc]
    public void RpctakeDamage(int damageAmount,string sourceID)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damageAmount;
        Debug.Log(transform.name + " dam " + currentHealth);

        if (currentHealth<=0)
        {
            Die(sourceID);
        }
    }

    private void Die(string sourceID)
    {
        isDead = true;

        player sourcePlayer = gameManager.getPlayer(sourceID);
        if(sourcePlayer != null)
        {
            sourcePlayer.kills++;
        }

        deaths++;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        StartCoroutine(respawn());
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(gameManager.instance.matchSettings.respawnTime);

        setDefaults();
        Transform startingPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startingPoint.position;
        transform.rotation = startingPoint.rotation;
    }
    private void setDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if(_col!= null)
        {
            _col.enabled = true;
        }
    }
}
