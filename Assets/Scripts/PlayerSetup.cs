using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentToDisable;
    private Camera sceneCamera;

    void Start()
    {
        sceneCamera = Camera.main;
        if (!isLocalPlayer)
            for (int i = 0; i < componentToDisable.Length; i++)
                componentToDisable[i].enabled = false;
        else
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }
}
