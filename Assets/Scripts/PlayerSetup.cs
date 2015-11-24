using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentToDisable;
    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    private Camera sceneCamera;

    void Start()
    {
        sceneCamera = Camera.main;
        if (!isLocalPlayer)
            DisableComponents();
        else
            if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }

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
}
