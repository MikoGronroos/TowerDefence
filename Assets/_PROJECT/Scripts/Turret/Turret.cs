using UnityEngine;
using Finark.Utils;
using System.Collections;
using Photon.Pun;
using System.Collections.Generic;

public class Turret : MonoBehaviour, IPunInstantiateMagicCallback
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretState currentState = TurretState.Idle;

    [SerializeField] private Transform target = null;

    [SerializeField] private List<Effect> currentEffects = new List<Effect>();

    private bool _shooting = false;

    private PhotonView _photonView;

    public int TurretOwnerID;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        turretStats.Damage.Value = turretStats.Damage.BaseValue;
        turretStats.Range.Value = turretStats.Damage.BaseValue;
        turretStats.AttackSpeed.Value = turretStats.Damage.BaseValue;
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, turretStats.Range.Value);

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

        target.GetComponent<IDamageable>().Damage(turretStats.Damage.Value, turretStats.Projectiles);

        yield return new WaitForSeconds(turretStats.AttackSpeed.Value);

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

    #region Effects

    public void AddEffect(Effect effect)
    {
        if (!currentEffects.Contains(effect))
        {
            currentEffects.Add(effect);
            CalculateEffects();
        }
    }

    public void RemoveEffect(Effect effect)
    {
        if (currentEffects.Contains(effect))
        {
            currentEffects.Remove(effect);
            CalculateEffects();
        }
    }

    public void CalculateEffects()
    {

        turretStats.Damage.Value = turretStats.Damage.BaseValue;
        turretStats.Range.Value = turretStats.Damage.BaseValue;
        turretStats.AttackSpeed.Value = turretStats.Damage.BaseValue;

        foreach (var newEffect in currentEffects)
        {

            var effect = newEffect as TurretEffect;

            switch (effect.EffectType)
            {
                case TurretEffectType.AttackSpeed:
                    turretStats.AttackSpeed.Value *= effect.Addon;
                    break;
                case TurretEffectType.Damage:
                    turretStats.Damage.Value *= effect.Addon;
                    break;
                case TurretEffectType.Range:
                    turretStats.Range.Value *= effect.Addon;
                    break;
            }
        }
    }

    #endregion

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
}
