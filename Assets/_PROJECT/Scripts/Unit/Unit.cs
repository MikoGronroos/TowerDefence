using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(FollowPath))]
public class Unit : MonoBehaviour
{

    [SerializeField] private UnitStats unitStats;

    [SerializeField] private float currentHealth;

    private FollowPath _followPath;

    private PhotonView _photonView;

    public Player UnitOwner;

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

    public void RemoveCurrentHealth(float amount, ProjectileType source)
    {
        if (AllowedToMakeDamage(source))
        {
            currentHealth -= amount;
        }
        CheckHealth(currentHealth);
    }

    private void CheckHealth(float health)
    {
        if (health <= 0)
        {
            PhotonNetwork.Destroy(_photonView);
        }
    }

    private bool AllowedToMakeDamage(ProjectileType type)
    {
        foreach (var projectileType in unitStats.ProjectileTypesAllowed)
        {
            if (projectileType == type)
            {
                return true;
            }
        }
        return false;
    }

    public UnitStats GetUnitStats()
    {
        return unitStats;
    }

}
