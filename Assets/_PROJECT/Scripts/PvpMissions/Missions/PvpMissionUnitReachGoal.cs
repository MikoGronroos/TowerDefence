using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Reach Goal")]
public class PvpMissionUnitReachGoal : PvpMission
{

    public int CurrentAmount;
    public int AmountNeeded;
    public int UnitID;

    public override void Evaluate()
    {

        if (CurrentAmount >= AmountNeeded)
        {
            PvpMissionManager.Instance.CompleteMission(this);
        }

    }

    public override void Load()
    {
        EventManager.SubscribeToEvent("OnUnitReachedGoal", OnUnitReachedGoal);
    }

    public override void Unload()
    {
        EventManager.UnsubscribeToEvent("OnUnitReachedGoal", OnUnitReachedGoal);
    }

    void OnUnitReachedGoal(Dictionary<string, object> message)
    {
        int unitID = (int)message["UnitID"];
        int goalOwnerID = (int)message["GoalOwnerID"];
        if (unitID == UnitID && PlayerManager.Instance.GetLocalPlayer().GetPlayerID() != goalOwnerID)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}
