using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{

    [SerializeField] private BarrackLogic barrackLogic;

    private bool _canSpawnUnits;

    private BarrackUI _barrackUI;

    private PhotonView _photonView;

    private void Awake()
    {
        _barrackUI = GetComponent<BarrackUI>();
        _photonView = gameObject.GetPhotonView();
    }

    public override void Start()
    {
        if (_photonView.IsMine)
        {
            BarrackTimerState barrackTimerState = new BarrackTimerState(barrackLogic, TimerIsFull, _barrackUI.UpdateTimerBar);
            BarrackSpawnState barrackSpawnState = new BarrackSpawnState(barrackLogic, UnitsHaveSpawned);

            AddTransition(barrackSpawnState, barrackTimerState, new List<Func<bool>> { CantSpawnUnits });
            AddTransition(barrackTimerState, barrackSpawnState, new List<Func<bool>> { CanSpawnUnits });

            SwitchState(barrackTimerState);

            base.Start();
        }
    }

    public override void Update()
    {
        if (_photonView.IsMine)
        {
            base.Update();
        }
    }

    private void TimerIsFull()
    {
        _canSpawnUnits = true;
    }

    private void UnitsHaveSpawned()
    {
        _canSpawnUnits = false;
    }

    private bool CanSpawnUnits()
    {
        return _canSpawnUnits;
    }

    private bool CantSpawnUnits()
    {
        return !_canSpawnUnits;
    }
}
