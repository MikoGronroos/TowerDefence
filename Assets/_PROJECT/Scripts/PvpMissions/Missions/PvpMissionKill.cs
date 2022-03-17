using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Kill")]
public class PvpMissionKill : PvpMission
{

    public int CurrentAmount;
    public int AmountNeeded;
    public int UnitID;

    [SerializeField] private UnitEventChannel unitEventChannel;

    public override void Evaluate()
    {
        if (CurrentAmount >= AmountNeeded)
        {
            PvpMissionManager.Instance.CompleteMission(this);
        }
    }

    public override void Load()
    {
        unitEventChannel.OnUnitKilled += OnUnitKilled;
    }

    public override void Unload()
    {
        unitEventChannel.OnUnitKilled -= OnUnitKilled;
    }

    void OnUnitKilled(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var id = (int)args["UnitID"];
        if (id == UnitID)
        {
            CurrentAmount++;
            Evaluate();
        }
    }

}
