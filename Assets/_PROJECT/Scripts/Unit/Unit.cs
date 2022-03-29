using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Finark.Events;

[RequireComponent(typeof(FollowPath))]
public class Unit : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private UnitStats unitStats;

    [SerializeField] private float currentHealth;

    [SerializeField] private UnitEventChannel unitEventChannel;

    private FollowPath _followPath;

    private PhotonView _photonView;

    public int UnitInstanceId { get; private set; }

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
        UnitInstanceId = Random.Range(0,99999999);
    }

    public void RemoveCurrentHealth(float amount, IEnumerable<ProjectileType> types)
    {
        if (AllowedToMakeDamage(types))
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

    private bool AllowedToMakeDamage(IEnumerable<ProjectileType> types)
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

        unitEventChannel.OnUnitKilled?.Invoke(new Dictionary<string, object> { 
            { "UnitID", unitStats.UnitID },
            { "InstanceID", UnitInstanceId } 
        });

        PhotonNetwork.Destroy(_photonView);

    }

    public UnitStats GetUnitStats()
    {
        return unitStats;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var photonView = gameObject.GetPhotonView();
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length == 1)
        {
            if (!photonView.IsMine)
            {
                photonView.TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
            }
            UnitOwnerID = (int)data[0];
        }
    }
}
