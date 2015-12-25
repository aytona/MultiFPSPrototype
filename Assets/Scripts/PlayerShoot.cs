using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

    #region Variables

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    private const string PLAYER_TAG = "Player";

    #endregion Variables

    #region Monobehaviour

    void Start()
    {
        if (cam == null)
            this.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    #endregion Monobehaviour

    #region Private Methods

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.transform.forward, out _hit, weapon.range, mask))
            if (_hit.collider.tag == PLAYER_TAG)
                CmdPlayerHit(_hit.collider.name, weapon.damage);
    }

    [Command]
    private void CmdPlayerHit(string _playerID, int _damage)
    {
        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }

    #endregion Private Methods
}
