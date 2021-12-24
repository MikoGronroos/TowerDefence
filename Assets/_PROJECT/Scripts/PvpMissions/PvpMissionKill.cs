using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Kill")]
public class PvpMissionKill : PvpMission
{

    public int CurrentAmount;
    public int AmountNeeded;
    public int UnitID;

    public override void Evaluate()
    {
        if (CurrentAmount >= AmountNeeded)
        {
            EventManager.UnsubscribeToEvent("OnUnitKilled", OnUnitKilled);
            PvpMissionManager.Instance.CompleteMission(this);
        }
    }

    public override void Load()
    {
        EventManager.SubscribeToEvent("OnUnitKilled", OnUnitKilled);
    }

    public override void Unload()
    {
        EventManager.UnsubscribeToEvent("OnUnitKilled", OnUnitKilled);
    }

    void OnUnitKilled(Dictionary<string, object> message)
    {
        var id = (int)message["UnitID"];
        if (id == UnitID)
        {
            CurrentAmount++;
            Evaluate();
        }
    }

}
