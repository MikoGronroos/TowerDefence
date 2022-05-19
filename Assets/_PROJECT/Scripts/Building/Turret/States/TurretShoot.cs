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

    private string _shootSoundID;

    private string _graphicMainKey;

    private bool _executing;

    public TurretShoot(TurretExecutable turretExecutable, Transform transform, Turret turret, string shootSoundID, string graphicMainKey)
    {
        _turretExecutable = turretExecutable;
        _transform = transform;
        _turret = turret;
        _shootSoundID = shootSoundID;
        _graphicMainKey = graphicMainKey;
    }

    public override void EnterState(StateMachine machine)
    {
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
    }

    private IEnumerator Execute()
    {

        _executing = true;

        _turretExecutable.Execute(new Dictionary<string, object>(){
                                {"Position", _transform.position},
                                {"TargetPosition", _turret.GetTarget().position},
                                {"Rotation", MyUtils.GetDirectionVector2(_transform.position, _turret.GetTarget().position)},
                                {"TurretExecutable", _turretExecutable},
                                {"ShootSoundID", _shootSoundID},
                                {"MainKey", _graphicMainKey}
        });

        _turret.SetTarget(null);

        _turret.ResetShot();

        _executing = false;

        yield return null;

    }

}