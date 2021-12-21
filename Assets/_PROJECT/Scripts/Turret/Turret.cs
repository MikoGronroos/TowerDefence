using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Finark.Utils;
using System.Collections;
using Photon.Pun;

public class Turret : MonoBehaviour
{

    [SerializeField] private TurretStats turretStats;

    [SerializeField] private TurretState currentState = TurretState.Idle;

    private bool _shooting = false;

    private Transform _target = null;

    public Player TurretOwner;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StateMachine();
        }
    }

    #region Target Methods

    private void GetValidTarget()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, turretStats.Range);

        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;


        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                if (TurretOwner != target.Owner())
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
            _target = nearestTarget;
        }

        if (!NoTarget()) currentState = TurretState.Agro;

    }

    private void FollowClosestTarget()
    {

        if (NoTarget()) return;

        var direction = MyUtils.GetDirectionVector2(transform.position, _target.position);

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

        _target.GetComponent<IDamageable>().Damage(turretStats.Damage, turretStats.Projectiles);

        yield return new WaitForSeconds(turretStats.AttackSpeed);

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

    private bool NoTarget()
    {
        return _target == null;
    }

    public TurretStats GetTurretStats()
    {
        return turretStats;
    }

}
