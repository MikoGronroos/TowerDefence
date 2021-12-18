using Photon.Pun;
using UnityEngine;
using PathCreation;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    private PhotonView _photonView;

    private PathCreator _pathCreator;

    private Vector3 _spawnPoint;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void SpawnUnit(string unitPrefabName, int id)
    {
        _photonView.RPC("RPCSpawnUnit", RpcTarget.MasterClient, unitPrefabName, id);
    }

    //Spawns the object only on hosts client
    [PunRPC]
    private void RPCSpawnUnit(string unitPrefabName, int id)
    {
        if (PhotonNetwork.IsMasterClient)
        {

            var path = PathManager.Instance.GetPathWithPlayerID(id);
            _pathCreator = path.ThisPath;
            _spawnPoint = path.PathStartPos.position;

            GameObject unit = PhotonNetwork.Instantiate(unitPrefabName, _spawnPoint, Quaternion.identity);
            unit.GetComponent<FollowPath>().SetPath(_pathCreator);
        }
    }

}
