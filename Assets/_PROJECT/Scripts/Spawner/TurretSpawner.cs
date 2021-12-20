using Photon.Pun;
using UnityEngine;

public class TurretSpawner : MonoBehaviourSingleton<TurretSpawner>
{

    public void SpawnTurret(string turretPrefabName)
    {
        GameObject turret = PhotonNetwork.Instantiate(turretPrefabName, Vector2.zero, Quaternion.identity);
    }

}
