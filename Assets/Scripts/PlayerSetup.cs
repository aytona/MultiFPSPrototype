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
    [SerializeField]
    private string dontDrawLayerName = "DontDraw";
    [SerializeField]
    private GameObject playerGraphics;

    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    private Camera sceneCamera;

    #endregion Variables

    #region Monobehaviour

    void Start()
    {
        if (!isLocalPlayer)
            DisableComponents();
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }

        GetComponent<Player>().Setup();
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);
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

    private void SetLayerRecursively(GameObject graphics, int newLayer)
    {
        graphics.layer = newLayer;

        foreach (Transform child in graphics.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    #endregion Private Methods
}
