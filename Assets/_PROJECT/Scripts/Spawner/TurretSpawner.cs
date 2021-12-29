using Photon.Pun;
using UnityEngine;

public class TurretSpawner : MonoBehaviourSingleton<TurretSpawner>
{

    public void SpawnTurret(string turretPrefabName, Vector3 point)
    {
        object[] data = new object[1];
        data[0] = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
        GameObject turret = PhotonNetwork.Instantiate(turretPrefabName, point, Quaternion.identity, 0, data);
        PlayerManager.Instance.GetLocalPlayer().AddTurret(turret.GetComponent<Turret>());
    }

}