using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Finark.Events;

[RequireComponent(typeof(FollowPath))]
public class Unit : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private UnitStats unitStats;

    [SerializeField] private float currentHealth;

    [SerializeField] private UnitEventChannel unitEventChannel;

    [SerializeField] private List<UnitEffect> currentEffects = new List<UnitEffect>();

    private FollowPath _followPath;

    private int _currentUnitHardness;

    private PhotonView _photonView;

    public int InstanceId { get; private set; }

    public string PrefabName { get; set; }

    public int UnitOwnerID;

    private void Awake()
    {
        _followPath = GetComponent<FollowPath>();
        _photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        currentHealth = unitStats.StartHealth;
        _currentUnitHardness = unitStats.MaxUnitHardness;
        _followPath.SetSpeed(unitStats.Speed);
    }

    public void RemoveCurrentHealth(float amount, IEnumerable<ProjectileType> types)
    {
        if (AllowedToMakeDamage(types))
        {
            currentHealth -= amount;
        }
        CheckHealth(currentHealth);
    }

    public bool CheckIfProjectilePenetrates(int penetration)
    {
        return _currentUnitHardness < penetration;
    }

    public void RemoveHardness(int penetration)
    {
        _currentUnitHardness -= penetration;
    }

    public int GetHardness()
    {
        return _currentUnitHardness;
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

        if (types == null) return false;

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
            { "InstanceID", InstanceId } 
        });

        if (_photonView.IsMine) UnitSpawner.Instance.DespawnUnit(gameObject);

    }

    public UnitStats GetUnitStats()
    {
        return unitStats;
    }

    public FollowPath GetFollowPath()
    {
        return _followPath;
    }

    #region Effects

    public void AddEffect(UnitEffect effect)
    {
        effect.StartEffect(this);
        currentEffects.Add(effect);
    }

    public void RemoveEffect(UnitEffect effect)
    {
        effect.StopEffect(this);
        currentEffects.Remove(effect);
    }

    public bool UnitAlreadyContainsEffectWithID(string id)
    {
        return currentEffects.Where(i => i.effectId == id).Count() > 0; 
    }

    #endregion

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var photonView = gameObject.GetPhotonView();
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length == 2)
        {
            UnitOwnerID = (int)data[0];
            InstanceId = (int)data[1];

            UnitSpawner.Instance.AddUnitToList(gameObject, InstanceId);
        }
    }
}
