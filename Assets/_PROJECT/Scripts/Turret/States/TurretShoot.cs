using Finark.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Finark.Utils;

public class TurretShoot : State
{

    private TurretExecutable _turretExecutable;
    private Transform _transform;
    private Transform _target;
    private Turret _turret;

    private bool _executing;

    public TurretShoot(TurretExecutable turretExecutable, Transform transform, Transform target, Turret turret)
    {
        _turretExecutable = turretExecutable;
        _transform = transform;
        _target = target;
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
        if (!_executing) CoroutineCaller.Instance.StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {

        _executing = true;

        _turretExecutable.Execute(new Dictionary<string, object>(){
                                {"Position", _transform.position},
                                {"TargetPosition", _target.position},
                                {"Rotation", MyUtils.GetDirectionVector2(_transform.position, _target.position)},
                                {"TurretExecutable", _turretExecutable} }
        );

        _turret.SetTarget(null);

        yield return new WaitForSeconds(_turretExecutable.AttackSpeed.Value);

        _executing = false;

    }

}