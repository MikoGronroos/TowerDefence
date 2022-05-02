using UnityEngine;
using Photon.Pun;
using System;
using System.Collections.Generic;
using Finark.Events;
using Finark.AI;
using Finark.Utils;

public partial class Turret : StateMachine, IPunInstantiateMagicCallback
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretExecutable turretExecutable;

    [SerializeField] private Transform target = null;

    [SerializeField] private UpgradePaths turretUpgradePaths;

    [SerializeField] private int[] turretUpgradePathIndex = new int[3];

    [SerializeField] private TurretEventChannel turretEventChannel;

    private PhotonView _photonView;

    [SerializeField] private float _currentTime = 0.0f;
    [SerializeField] private bool _canShoot = false;

    public int TurretOwnerID;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public override void Start()
    {

        TurretSearching turretSearching = new TurretSearching(transform, turretExecutable, TurretOwnerID, this);
        TurretShoot turretShoot = new TurretShoot(turretExecutable, transform, this);

        AddAnyTransition(turretSearching, new List<Func<bool>> { CantShoot, NoTarget });
        AddAnyTransition(turretShoot, new List<Func<bool>> { CanShoot, HasTarget });

        SwitchState(turretSearching);

        RefreshValues();

        _currentTime = turretExecutable.AttackSpeed.Value;
        _canShoot = true;

    }

    public override void Update()
    {
        if (_photonView.IsMine)
        {
            Timer();
            FollowClosestTarget();
            base.Update();
        }
    }

    private void Timer()
    {

        if (_canShoot) return;

        _currentTime = Mathf.Clamp(_currentTime += Time.deltaTime, 0, turretExecutable.AttackSpeed.Value);

        if (_currentTime >= turretExecutable.AttackSpeed.Value)
        {
            _canShoot = true;
        }

    }

    public void SetTarget(Transform nearestTarget)
    {
        if (target == nearestTarget) return;
        target = nearestTarget;
    }

    public void ResetShot()
    {
        _currentTime = 0.0f;
        _canShoot = false;
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

    private void FollowClosestTarget()
    {

        if (target == null) return;

        if (!turretExecutable.FollowsTarget) return;

        var direction = MyUtils.GetDirectionVector2(transform.position, target.position);

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }

    #region State Machine Conditions

    private bool HasTarget()
    {
        return target != null;
    }

    private bool CanShoot()
    {
        return _canShoot;
    }

    private bool CantShoot()
    {
        return !_canShoot;
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
