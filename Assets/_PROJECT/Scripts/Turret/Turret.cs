using UnityEngine;
using Finark.Utils;
using System.Collections;
using Photon.Pun;
using System;

public class Turret : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretState currentState = TurretState.Idle;

    [SerializeField] private TurretExecutable turretExecutable;

    [SerializeField] private Transform target = null;

    [SerializeField] private UpgradePaths turretUpgradePaths;

    [SerializeField] private int[] turretUpgradePathIndex = new int[3];

    private bool _executing = false;

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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, turretExecutable.Range.Value);

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

    #region Execute Methods

    private IEnumerator Execute()
    {

        if (NoTarget() && turretExecutable.NeedsTarget)
        {
            currentState = TurretState.Idle;
            yield break;
        }

        _executing = true;

        turretExecutable.Execute();

        yield return new WaitForSeconds(turretExecutable.AttackSpeed.Value);

        _executing = false;

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
                if (!_executing) StartCoroutine(Execute());
                break;
        }
    }

    #endregion

    #region Turret Upgrade Paths

    public void UpgaredTurret(int turretPathIndex)
    {

        var upgrade = turretUpgradePaths.Paths[turretPathIndex].Upgrades[turretUpgradePathIndex[turretPathIndex]];

        if (!VirtualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(upgrade.Price)) return;

        VirtualCurrencyManager.Instance.RemoveCurrency(upgrade.Price);

        upgrade.UseUpgrade(this);
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

    public void AddNewTurretExecutable(TurretExecutable executable)
    {
        turretExecutable = executable;
    }

    public TurretExecutable GetTurretExecutable()
    {
        return turretExecutable;
    }

    #endregion

    private void RefreshValues()
    {
        turretExecutable.Damage.Value = turretExecutable.Damage.BaseValue;
        turretExecutable.Range.Value = turretExecutable.Range.BaseValue;
        turretExecutable.AttackSpeed.Value = turretExecutable.AttackSpeed.BaseValue;
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
