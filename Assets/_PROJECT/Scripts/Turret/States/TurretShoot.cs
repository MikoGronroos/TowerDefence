using Finark.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Finark.Utils;

public class TurretShoot : State
{

    private TurretExecutable _turretExecutable;
    private Transform _transform;
    private Turret _turret;

    private bool _executing;

    public TurretShoot(TurretExecutable turretExecutable, Transform transform, Turret turret)
    {
        _turretExecutable = turretExecutable;
        _transform = transform;
        _turret = turret;
    }

    public override void EnterState(StateMachine machine)
    {
        Debug.Log("Entered Turret Shoot");
        if (!_executing) CoroutineCaller.Instance.StartCoroutine(Execute());
    }

    public override void ExitState(StateMachine machine)
    {
    }

    public override void PhysicsRunState(StateMachine machine)
    {
    }

    public override void RunState(StateMachine machine)
    {
        Debug.Log("Running Turret Shoot");
    }

    private IEnumerator Execute()
    {

        _executing = true;

        _turretExecutable.Execute(new Dictionary<string, object>(){
                                {"Position", _transform.position},
                                {"TargetPosition", _turret.GetTarget().position},
                                {"Rotation", MyUtils.GetDirectionVector2(_transform.position, _turret.GetTarget().position)},
                                {"TurretExecutable", _turretExecutable}
        });

        _turret.SetTarget(null);

        _turret.ResetShot();

        _executing = false;

        yield return null;

    }

}