using Photon.Pun;
using UnityEngine;
using PathCreation;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    private PathCreator _pathCreator;

    private Vector3 _spawnPoint;

    public void SpawnUnit(string unitPrefabName, int id)
    {
        var path = PathManager.Instance.GetPathWithPlayerID(id);
        _pathCreator = path.ThisPath;
        _spawnPoint = path.PathStartPos.position;

        GameObject unit = PhotonNetwork.Instantiate(unitPrefabName, _spawnPoint, Quaternion.identity);
        unit.GetComponent<FollowPath>().SetPath(_pathCreator);
        unit.GetComponent<Unit>().UnitOwnerID = PlayerManager.Instance.GetLocalPlayer().GetPlayerID();
    }

}
