using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Money Spending")]
public class PvpMissionUseMoney : PvpMission
{

    public int CurrentAmount;
    public int NeededAmount;

    public override void Evaluate()
    {
        if (CurrentAmount >= NeededAmount)
        {
            PvpMissionManager.Instance.CompleteMission(this);
        }
    }

    public override void Load()
    {
        EventManager.SubscribeToEvent("OnMoneyUsed", OnMoneyUsed);
    }

    public override void Unload()
    {
        EventManager.UnsubscribeToEvent("OnMoneyUsed", OnMoneyUsed);
    }

    private void OnMoneyUsed(Dictionary<string, object> message)
    {
        var money = (int)message["money"];

        CurrentAmount += money;
        Evaluate();
    }

}
