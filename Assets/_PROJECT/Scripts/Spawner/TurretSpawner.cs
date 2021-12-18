using Photon.Pun;
using UnityEngine;

public class TurretSpawner : MonoBehaviourSingleton<TurretSpawner>
{

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void SpawnTurret(string turretPrefabName)
    {
        _photonView.RPC("RPCSpawnTurret", RpcTarget.MasterClient, turretPrefabName);
    }

    //Spawns the object only on hosts client
    [PunRPC]
    private void RPCSpawnTurret(string turretPrefabName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject turret = PhotonNetwork.Instantiate(turretPrefabName, Vector2.zero, Quaternion.identity);
        }
    }
}
