using Finark.AI;
using System;
using System.Collections;
using UnityEngine;

public class BarrackSpawnState : State
{

    private BarrackLogic _barrackLogic;
    private Action _callback;

    private bool _isRunning;

    public BarrackSpawnState(BarrackLogic barrackLogic, Action callback) 
    {
        _barrackLogic = barrackLogic;
        _callback = callback;
    }

    public override void EnterState(StateMachine machine) {  }

    public override void ExitState(StateMachine machine) {   }

    public override void PhysicsRunState(StateMachine machine) { }

    public override void RunState(StateMachine machine) 
    {
        if(!_isRunning) CoroutineCaller.Instance.StartChildCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {

        _isRunning = true;

        WaitForSeconds wait = new WaitForSeconds(0.45f);
        for (int i = 0; i < _barrackLogic.SpawnAmount; i++)
        {
            UnitSpawner.Instance.RequestUnitSpawn(_barrackLogic.UnitPrefab.name, PlayerManager.Instance.GetLocalPlayer().GetPlayerID());
            yield return wait;
        }

        _callback?.Invoke();

        _isRunning = false;
    }

}