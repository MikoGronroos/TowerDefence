using Finark.Pooling;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSpawner : MonoBehaviourSingleton<UnitSpawner>
{

    private Dictionary<string, PoolList> poolLists = new Dictionary<string, PoolList>();

    [SerializeField] private List<PoolObject> allUnits = new List<PoolObject>();
    
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    #region Spawn Unit

    public void RequestUnitSpawn(string unitPrefabName, int id)
    {
        _photonView.RPC("RPCSpawnUnit", RpcTarget.All, unitPrefabName, id);
    }

    [PunRPC]
    private void RPCSpawnUnit(string unitPrefabName, int id)
    {
        SpawnUnit(unitPrefabName, id);
    }

    private void SpawnUnit(string unitPrefabName, int id)
    {
        var path = PathManager.Instance.GetPathWithPlayerID(id);

        object[] data = new object[2];
        data[0] = id;
        data[1] = Random.Range(0, 999999999);

        GameObject unit;

        if (id != PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
        {
            if (poolLists.ContainsKey(unitPrefabName) && !poolLists[unitPrefabName].IsEmpty())
            {
                unit = poolLists[unitPrefabName].Dequeue();
                var currentPath = unit.GetComponent<FollowPath>();
                currentPath.ResetProgress();
                unit.transform.position = path.PathStartPos.position;
                _photonView.RPC("RPCToggleGameObject", RpcTarget.All, unit.GetComponent<Unit>().InstanceId, true);
            }
            else
            {
                unit = PhotonNetwork.Instantiate($"Units/{unitPrefabName}", path.PathStartPos.position, Quaternion.identity, 0, data);
                unit.GetComponent<Unit>().PrefabName = unitPrefabName;
                var currentPath = unit.GetComponent<FollowPath>();
                currentPath.SetPath(path.ThisPath);
            }

        }

    }

    #endregion

    #region Despawn Unit

    public void DespawnUnit(GameObject unit)
    {

        Unit despawningUnit = unit.GetComponent<Unit>();

        if (!poolLists.ContainsKey(despawningUnit.PrefabName))
        {
            poolLists.Add(despawningUnit.PrefabName, new PoolList());
        }

        poolLists[despawningUnit.PrefabName].Enqueue(unit);

        _photonView.RPC("RPCToggleGameObject", RpcTarget.All, despawningUnit.InstanceId, false);

    }

    #endregion

    public void AddUnitToList(GameObject unit, int instanceId)
    {
        allUnits.Add(new PoolObject(instanceId, unit));
    }

    [PunRPC]
    private void RPCToggleGameObject(int instanceId, bool toggleStatus)
    {
        allUnits.Where(i => i.instanceId == instanceId).ToArray()[0].Go.SetActive(toggleStatus);
    }

}