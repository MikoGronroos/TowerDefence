using Photon.Pun;
using UnityEngine;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    public void SpawnUnit(string unitPrefabName, int id)
    {
        var path = PathManager.Instance.GetPathWithPlayerID(id);

        object[] data = new object[1];
        data[0] = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
        GameObject unit = PhotonNetwork.Instantiate(unitPrefabName, path.PathStartPos.position, Quaternion.identity, 0, data);
        unit.GetComponent<FollowPath>().SetPath(path.ThisPath);
    }

}
