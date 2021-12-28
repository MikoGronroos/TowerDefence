using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

[RequireComponent(typeof(FollowPath))]
public class Unit : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private UnitStats unitStats;

    [SerializeField] private float currentHealth;

    private FollowPath _followPath;

    private PhotonView _photonView;

    public int UnitOwnerID;

    private void Awake()
    {
        _followPath = GetComponent<FollowPath>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        currentHealth = unitStats.StartHealth;
        _followPath.SetSpeed(unitStats.Speed);
    }

    public void RemoveCurrentHealth(float amount, ProjectileType[] projectiles)
    {
        if (AllowedToMakeDamage(projectiles))
        {
            currentHealth -= amount;
        }
        CheckHealth(currentHealth);
    }

    private void CheckHealth(float health)
    {
        if (health <= 0)
        {
            OnUnitDestroyed();
        }
    }

    private bool AllowedToMakeDamage(ProjectileType[] types)
    {
        foreach (var projectileType in unitStats.ProjectileTypesAllowed)
        {
            foreach (var type in types)
            {
                if (projectileType == type)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnUnitDestroyed()
    {

        PlayerLevel.Instance.AddXp(unitStats.XpAddonOnDestroyed);

        EventManager.InvokeEvent("OnUnitKilled", new Dictionary<string, object> { { "UnitID", unitStats.UnitID } });

        PhotonNetwork.Destroy(_photonView);

    }

    public UnitStats GetUnitStats()
    {
        return unitStats;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = this.gameObject.GetPhotonView().InstantiationData;
        if (data != null && data.Length == 1)
        {
            UnitOwnerID = (int)data[0];
        }
    }
}
