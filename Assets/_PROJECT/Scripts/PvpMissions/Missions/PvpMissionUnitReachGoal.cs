using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Reach Goal")]
public class PvpMissionUnitReachGoal : PvpMission
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
        unitEventChannel.OnUnitReachedGoal += OnUnitReachedGoal;
    }

    public override void Unload()
    {
        unitEventChannel.OnUnitReachedGoal -= OnUnitReachedGoal;
    }

    void OnUnitReachedGoal(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        int unitID = (int)args["UnitID"];
        int goalOwnerID = (int)args["GoalOwnerID"];
        if (unitID == UnitID && PlayerManager.Instance.GetLocalPlayer().GetPlayerID() != goalOwnerID)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
