using Photon.Pun;
using UnityEngine;

public class TurretSpawner : MonoBehaviourSingleton<TurretSpawner>
{

    public void SpawnTurret(string turretPrefabName, Vector3 point)
    {
        GameObject turret = PhotonNetwork.Instantiate(turretPrefabName, point, Quaternion.identity);
        turret.GetComponent<Turret>().TurretOwnerID = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
    }
}