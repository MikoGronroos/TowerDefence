using Finark.AI;
using Finark.Utils;
using UnityEngine;

public class TurretAim : State
{

    private Transform _transform;
    private Turret _turret;

    public TurretAim(Transform transform, Turret turret)
    {
        _transform = transform;
        _turret = turret;
    }

    public override void EnterState(StateMachine machine)
    {
    }

    public override void ExitState(StateMachine machine)
    {
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
        FollowClosestTarget();
    }

    private void FollowClosestTarget()
    {

        var direction = MyUtils.GetDirectionVector2(_transform.position, _turret.GetTarget().position);

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }

}
