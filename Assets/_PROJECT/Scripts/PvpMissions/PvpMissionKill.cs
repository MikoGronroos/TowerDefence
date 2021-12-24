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
            PvpMissionManager.Instance.CompleteMission(this);
        }
    }

    public override void Init()
    {
        EventManager.SubscribeToEvent("OnUnitKilled", OnUnitKilled);
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
