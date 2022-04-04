using UnityEngine;
using Finark.Utils;
using System.Collections;
using Photon.Pun;
using System;
using System.Collections.Generic;
using Finark.Events;
using Finark.AI;

public partial class Turret : StateMachine, IPunInstantiateMagicCallback
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretExecutable turretExecutable;

    [SerializeField] private Transform target = null;

    [SerializeField] private UpgradePaths turretUpgradePaths;

    [SerializeField] private int[] turretUpgradePathIndex = new int[3];

    [SerializeField] private TurretEventChannel turretEventChannel;

    private PhotonView _photonView;

    public int TurretOwnerID;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public override void Start()
    {

        TurretSearching turretSearching = new TurretSearching(transform, turretExecutable, TurretOwnerID, this);
        TurretAim turretAim = new TurretAim(transform, this);
        TurretShoot turretShoot = new TurretShoot(turretExecutable, transform, target, this);

        AddTransition(turretSearching, turretAim, HasTarget);
        AddTransition(turretAim, turretSearching, TargetOutOfReach);
        AddTransition(turretShoot, turretSearching, TargetOutOfReach);
        AddTransition(turretSearching, turretShoot, false);
        AddAnyTransition(turretSearching, NoTarget);

        SwitchState(turretSearching);

        RefreshValues();
    }

    public override void Update()
    {
        if (_photonView.IsMine)
        {
            base.Update();
        }
    }

    public void SetTarget(Transform nearestTarget)
    {
        target = nearestTarget;
    }

    #region Turret Upgrade Paths

    public void UpgaredTurret(int turretPathIndex)
    {

        if (UpgradePathFullyUpgraded(turretPathIndex))
        {
            return;
        }

        var upgrade = turretUpgradePaths.Paths[turretPathIndex].Upgrades[turretUpgradePathIndex[turretPathIndex]];

        if (!VirtualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(upgrade.Price)) return;

        VirtualCurrencyManager.Instance.RemoveCurrency(upgrade.Price);

        upgrade.UseUpgrade(this);
        turretUpgradePathIndex[turretPathIndex]++;
        RefreshValues();

        turretEventChannel.OnTurretUpgraded?.Invoke(new Dictionary<string, object> { { "turret", this } });

    }

    public UpgradePaths GetUpgradePaths()
    {
        return turretUpgradePaths;
    }

    public bool UpgradePathFullyUpgraded(int turretPathIndex)
    {
        return turretUpgradePathIndex[turretPathIndex] >= turretUpgradePaths.Paths[turretPathIndex].Upgrades.Length;
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

    #region State Machine Conditions

    private bool HasTarget()
    {
        return target != null;
    }

    private bool TargetOutOfReach()
    {
        return Vector3.Distance(transform.position, target.position) > (turretExecutable.Range.Value / 2);
    }

    private bool Shoot()
    {
        return true;
    }

    private bool NoTarget()
    {
        return target == null;
    }

    #endregion

    public Transform GetTarget()
    {
        return target;
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
