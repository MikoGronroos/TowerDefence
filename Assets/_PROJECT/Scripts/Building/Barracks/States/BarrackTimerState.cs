using Finark.AI;
using System;
using UnityEngine;

public class BarrackTimerState : State
{

    private BarrackLogic _barrackLogic;

    private float _currentTime;
    private Action _callback;
    private Action<float, float> _timerUI;

    public BarrackTimerState(BarrackLogic barrackLogic, Action callback, Action<float,float> timerUI)
    {
        _barrackLogic = barrackLogic;
        _callback = callback;
        _timerUI = timerUI;
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
        Timer();
    }

    private void Timer()
    {
        _currentTime = Mathf.Clamp(_currentTime += Time.deltaTime, 0, _barrackLogic.SpawnInterval);

        _timerUI?.Invoke(_currentTime, _barrackLogic.SpawnInterval);

        if (_currentTime >= _barrackLogic.SpawnInterval)
        {
            _currentTime = 0f;
            _callback?.Invoke();
        }
    }

}