using Photon.Pun;
using UnityEngine;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    public void SpawnUnit(string unitPrefabName, int id)
    {
        Debug.Log($"Spawning Unit: {unitPrefabName} {id}");
        var path = PathManager.Instance.GetPathWithPlayerID(id);

        GameObject unit = PhotonNetwork.Instantiate(unitPrefabName, path.PathStartPos.position, Quaternion.identity);
        unit.GetComponent<FollowPath>().SetPath(path.ThisPath);
        unit.GetComponent<Unit>().UnitOwnerID = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
    }

}
