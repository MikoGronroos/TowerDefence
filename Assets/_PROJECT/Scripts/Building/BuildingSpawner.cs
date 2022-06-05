using Photon.Pun;
using UnityEngine;

public class BuildingSpawner : MonoBehaviourSingleton<BuildingSpawner>
{

    [SerializeField] private string path;

    public GameObject SpawnBuilding(string prefabName, Vector3 point)
    {
        object[] data = new object[1];
        data[0] = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
        GameObject building = PhotonNetwork.Instantiate(path + prefabName, point, Quaternion.identity, 0, data);
        return building;
    }

}