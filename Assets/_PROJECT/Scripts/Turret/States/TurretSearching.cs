using Finark.AI;
using UnityEngine;

public class TurretSearching : State
{

    private Transform _transform;
    private TurretExecutable _turretExecutable;
    private int _turretOwnerID;
    private Turret _turret;

    public TurretSearching(Transform transform, TurretExecutable turretExecutable, int turretOwnerID, Turret turret)
    {
        _transform = transform;
        _turretExecutable = turretExecutable;
        _turretOwnerID = turretOwnerID;
        _turret = turret;
    }

    public override void EnterState(StateMachine machine)
    {
        _turret.SetTarget(null);
        Debug.Log("Entered Turret Searching");
    }

    public override void ExitState(StateMachine machine) { }

    public override void PhysicsRunState(StateMachine machine) { }

    public override void RunState(StateMachine machine)
    {
        Debug.Log("Running Turret Searching");
        GetValidTarget();
    }

    private void GetValidTarget()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _turretExecutable.Range.Value);

        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;


        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                if (_turretOwnerID != target.OwnerID())
                {
                    float distance = Vector3.Distance(_transform.position, target.Position());
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
            _turret.SetTarget(nearestTarget);
        }

    }

}

