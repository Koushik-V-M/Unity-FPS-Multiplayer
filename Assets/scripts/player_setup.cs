using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(player ))]
public class player_setup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "remotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
    public GameObject playerUIInstance;


    Camera sceneCamera;
    void Start()
    {
        if (!isLocalPlayer)
        {
            disableComponents();
            remotePlayerSetup();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            playerUI UI = playerUIInstance.GetComponent<playerUI>();
            if (UI == null)
            {
                Debug.LogError("no playerUI component on playerUI");
            }
            UI.setPlayer(GetComponent<player>());

            GetComponent<player>().setup();

            string _username = "Loading...";
            if (UserAccountManager.IsLoggedIn)
            {
                _username = UserAccountManager.PlayerUsername;
            }
            else
            {
                _username = transform.name;
            }
            CmdSetUsername(transform.name, _username);
        }
        
    }

    [Command]
    void CmdSetUsername(string playerID,string username)
    {
        player _player = gameManager.getPlayer(playerID);
        if (_player != null)
        {
            _player.username = username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netid = GetComponent<NetworkIdentity>().netId.ToString();
        player _player = GetComponent<player>();
        gameManager.RegisterPlayer(_netid, _player);
    }
    void disableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void remotePlayerSetup()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        gameManager.unRegisterPlayer(transform.name);
    }
}
