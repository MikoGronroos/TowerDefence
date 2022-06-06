using UnityEngine;
using Finark.Pooling;
using System.Collections.Generic;
using Photon.Pun;
using System.Linq;

public class ProjectileSpawner : MonoBehaviourSingleton<ProjectileSpawner>
{

    [SerializeField] private string path;

    private Dictionary<string, PoolList> poolLists = new Dictionary<string, PoolList>();

    [SerializeField] private List<PoolObject> allProjectiles = new List<PoolObject>();

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = gameObject.GetPhotonView();
    }

    #region Spawning Projectile

    public void RequestProjectileSpawn(string projectilePrefabName, Vector3 position, Vector2 rotation, TurretExecutable exec = null, string mainKey = null)
    {
        SpawnProjectile(projectilePrefabName, position, rotation, exec, mainKey);
    }

    #endregion

    private void SpawnProjectile(string projectilePrefabName, Vector3 position, Vector2 rotation, TurretExecutable exec, string mainKey)
    {
        object[] data = new object[1];
        data[0] = Random.Range(0, 999999999);

        GameObject projectile;

        if (poolLists.ContainsKey(projectilePrefabName) && !poolLists[projectilePrefabName].IsEmpty())
        {
            Debug.Log("Reused an old one");
            projectile = poolLists[projectilePrefabName].Dequeue();
            projectile.transform.position = position;
            if (projectile.TryGetComponent(out Projectile proj))
            {
                proj.Setup(rotation, exec, mainKey);
                _photonView.RPC("RPCToggleGameObject", RpcTarget.All, proj.InstanceId, true);
            }
        }
        else
        {
            Debug.Log("Spawned new");
            projectile = PhotonNetwork.Instantiate($"{path}{projectilePrefabName}", position, Quaternion.identity, 0, data);
            if (projectile.TryGetComponent(out Projectile proj))
            {
                proj.Setup(rotation, exec, mainKey);
                proj.PrefabName = projectilePrefabName;
            }
        }

    }

    public void DespawnUnit(GameObject projectile)
    {

        Projectile despawningProjectile = projectile.GetComponent<Projectile>();

        if (despawningProjectile == null) { PhotonNetwork.Destroy(projectile.GetPhotonView()); return; }

        if (!poolLists.ContainsKey(despawningProjectile.PrefabName))
        {
            poolLists.Add(despawningProjectile.PrefabName, new PoolList());
        }

        poolLists[despawningProjectile.PrefabName].Enqueue(projectile);

        _photonView.RPC("RPCToggleGameObject", RpcTarget.All, despawningProjectile.InstanceId, false);

    }
    public void AddProjectileToList(GameObject projectile, int instanceId)
    {
        allProjectiles.Add(new PoolObject(instanceId, projectile));
    }

    [PunRPC]
    private void RPCToggleGameObject(int instanceId, bool toggleStatus)
    {
        allProjectiles.Where(i => i.instanceId == instanceId).ToArray()[0].Go.SetActive(toggleStatus);
    }


}
