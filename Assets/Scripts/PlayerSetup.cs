using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentToDisable;
    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    private Camera sceneCamera;

    void Start()
    {
        RegisterPlayer();
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

    private void DisableComponents()
    {
        for (int i = 0; i < componentToDisable.Length; i++)
            componentToDisable[i].enabled = false;
        AssignRemoteLayer();
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }
}
