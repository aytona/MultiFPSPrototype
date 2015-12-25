using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{

    #region Variables

    [SerializeField]
    private Behaviour[] componentToDisable;
    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    private Camera sceneCamera;

    #endregion Variables

    #region Monobehaviour

    void Start()
    {
        sceneCamera = Camera.main;
        if (!isLocalPlayer)
            DisableComponents();
        else
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);

        GetComponent<Player>().Setup();
    }

    void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }

    #endregion Monobehaviour

    #region Public Methods

    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }

    #endregion Public Methods

    #region Private Methods

    private void DisableComponents()
    {
        for (int i = 0; i < componentToDisable.Length; i++)
            componentToDisable[i].enabled = false;
        AssignRemoteLayer();
        GameManager.DeregisterPlayer(transform.name);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    #endregion Private Methods
}
