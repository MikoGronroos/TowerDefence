using Photon.Pun;
using UnityEngine;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void RequestUnitSpawn(string unitPrefabName, int id)
    {
        if (GameSettingsManager.Instance.GetGameSettings().Singleplayer)
        {
            //When game is singleplayer
            SpawnUnit(unitPrefabName, id);
        }
        else
        {
            //When game is multiplayer
            _photonView.RPC("RPCSpawnUnit", RpcTarget.All, unitPrefabName, id);
        }
    }

    [PunRPC]
    private void RPCSpawnUnit(string unitPrefabName, int id)
    {
        if (id != PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
        {
            SpawnUnit(unitPrefabName, id);
        }
    }

    private void SpawnUnit(string unitPrefabName, int id)
    {
        var path = PathManager.Instance.GetPathWithPlayerID(id);

        object[] data = new object[1];
        data[0] = id;
        GameObject unit = PhotonNetwork.Instantiate(unitPrefabName, path.PathStartPos.position, Quaternion.identity, 0, data);
        unit.GetComponent<FollowPath>().SetPath(path.ThisPath);
    }

}
