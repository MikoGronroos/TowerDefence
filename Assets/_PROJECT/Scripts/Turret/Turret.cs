using UnityEngine;
using Finark.Utils;
using System.Collections;
using Photon.Pun;
using System;

public class Turret : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretState currentState = TurretState.Idle;

    [SerializeField] private TurretExecutable primaryExecutable;

    [SerializeField] private TurretExecutable secondaryExecutable;

    [SerializeField] private Transform target = null;

    [SerializeField] private UpgradePaths turretUpgradePaths;

    [SerializeField] private int[] turretUpgradePathIndex = new int[3];

    private bool _shooting = false;

    private PhotonView _photonView;

    public int TurretOwnerID;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        RefreshValues();
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            StateMachine();
        }
    }

    #region Target Methods

    private void GetValidTarget()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, primaryExecutable.Range.Value);

        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;


        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                if (TurretOwnerID != target.OwnerID())
                {
                    float distance = Vector3.Distance(transform.position, target.Position());
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestTarget = collider.transform;
                    }
                }
            }
        }

        if (nearestTarget != null)
        {
            target = nearestTarget;
        }

        if (!NoTarget()) currentState = TurretState.Agro;

    }

    private void FollowClosestTarget()
    {

        if (NoTarget()) return;

        var direction = MyUtils.GetDirectionVector2(transform.position, target.position);

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }

    #endregion

    #region Shoot Methods

    private IEnumerator Shoot()
    {

        if (NoTarget())
        {
            currentState = TurretState.Idle;
            yield break;
        }

        _shooting = true;

        target.GetComponent<IDamageable>().Damage(primaryExecutable.Damage.Value, primaryExecutable.ProjectileTypes);

        yield return new WaitForSeconds(primaryExecutable.AttackSpeed.Value);

        _shooting = false;
    }

    #endregion

    #region State Machine

    private void StateMachine()
    {
        switch (currentState)
        {
            case TurretState.Idle:
                InvokeRepeating("GetValidTarget", 0f, 0.5f);
                currentState = TurretState.Searching;
                break;
            case TurretState.Searching:
                break;
            case TurretState.Agro:
                if (NoTarget()) currentState = TurretState.Idle;
                CancelInvoke();
                FollowClosestTarget();
                if (!_shooting) StartCoroutine(Shoot());
                break;
        }
    }

    #endregion

    #region Turret Upgrade Paths

    public void UpgaredTurret(int turretPathIndex)
    {
        turretUpgradePaths.Paths[turretPathIndex].Upgrades[turretUpgradePathIndex[turretPathIndex]].UseUpgrade(this);
        turretUpgradePathIndex[turretPathIndex]++;
        RefreshValues();
    }

    public UpgradePaths GetUpgradePaths()
    {
        return turretUpgradePaths;
    }

    public int[] GetTurretPathIndex()
    {
        return turretUpgradePathIndex;
    }

    #endregion

    #region Turret Executable

    public void AddNewPrimaryExecutable(TurretExecutable executable)
    {
        primaryExecutable = executable;
    }

    public void AddNewSecondaryExecutable(TurretExecutable executable)
    {
        secondaryExecutable = executable;
    }

    public TurretExecutable GetPrimaryTurretExecutable()
    {
        return primaryExecutable;
    }

    public TurretExecutable GetSecondaryTurretExecutable()
    {
        return secondaryExecutable;
    }

    #endregion

    private void RefreshValues()
    {
        primaryExecutable.Damage.Value = primaryExecutable.Damage.BaseValue;
        primaryExecutable.Range.Value = primaryExecutable.Range.BaseValue;
        primaryExecutable.AttackSpeed.Value = primaryExecutable.AttackSpeed.BaseValue;
    }

    private bool NoTarget()
    {
        return target == null;
    }

    public TurretStats GetTurretStats()
    {
        return turretStats;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] data = this.gameObject.GetPhotonView().InstantiationData;
        if (data != null && data.Length == 1)
        {
            TurretOwnerID = (int)data[0];
        }
    }

    private void OnValidate()
    {
        if (turretUpgradePathIndex.Length != 3)
        {
            Debug.LogWarning("Don't change the 'Paths' field's array size!");
            Array.Resize(ref turretUpgradePathIndex, 3);
        }
    }

}
