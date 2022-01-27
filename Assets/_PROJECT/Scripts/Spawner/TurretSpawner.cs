using Photon.Pun;
using UnityEngine;

public class TurretSpawner : MonoBehaviourSingleton<TurretSpawner>
{

    [SerializeField] private string path;

    [SerializeField] private Transform turretsParent;

    public Turret SpawnTurret(string turretPrefabName, Vector3 point)
    {
        object[] data = new object[1];
        data[0] = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
        GameObject turret = PhotonNetwork.Instantiate(path + turretPrefabName, point, Quaternion.identity, 0, data);
        turret.transform.SetParent(turretsParent);
        return turret.GetComponent<Turret>();
    }

}