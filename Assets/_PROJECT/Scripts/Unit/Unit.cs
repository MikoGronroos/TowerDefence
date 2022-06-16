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

    [Header("Effects")]

    [SerializeField] private List<UnitEffectWrapper> currentEffects = new List<UnitEffectWrapper>();

    private FollowPath _followPath;

    private int _currentUnitHardness;

    private PhotonView _photonView;

    public int InstanceId { get; private set; }

    public string PrefabName { get; set; }

    public int UnitOwnerID;

    #region MonoBehaviour Methods

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

    private void Update()
    {
        if (!currentEffects.Any()) return;

        for (int i = currentEffects.Count - 1; i >= 0; i--)
        {
            currentEffects[i].Effect.UpdateEffect(this);
        }
    }

    #endregion

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

        RemoveAllEffects();

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

        object[] data = new object[1];
        data[0] = InstanceId;

        var effectGameObject = PhotonNetwork.Instantiate($"Effects/{effect.effectPrefab.name}", transform.position, Quaternion.identity, 0, data);
        currentEffects.Add(new UnitEffectWrapper(effect, effectGameObject));
    }

    public void RemoveEffect(UnitEffect effect)
    {
        var effectWrapper = GetEffectWrapperWithEffect(effect);

        effectWrapper.Effect.StopEffect(this);
        PhotonNetwork.Destroy(effectWrapper.EffectGameObject);
        currentEffects.Remove(effectWrapper);
    }

    public void RemoveAllEffects()
    {
        for (int i = currentEffects.Count - 1; i >= 0; i--)
        {
            RemoveEffect(currentEffects[i].Effect);  
        }
        currentEffects.Clear();
    }

    public bool UnitAlreadyContainsEffectWithID(string id)
    {
        return currentEffects.Where(i => i.Effect.effectId == id).Count() > 0; 
    }

    private UnitEffectWrapper GetEffectWrapperWithEffect(UnitEffect effect)
    {
        foreach (var target in currentEffects)
        {
            if (target.Effect == effect)
            {
                return target;
            }
        }
        return null;
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
