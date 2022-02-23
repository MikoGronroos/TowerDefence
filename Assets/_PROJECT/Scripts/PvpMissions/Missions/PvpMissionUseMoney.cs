using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Money Spending")]
public class PvpMissionUseMoney : PvpMission
{

    public int CurrentAmount;
    public int NeededAmount;

    [SerializeField] private CurrencyEventChannel currencyEventChannel;

    public override void Evaluate()
    {
        if (CurrentAmount >= NeededAmount)
        {
            PvpMissionManager.Instance.CompleteMission(this);
        }
    }

    public override void Load()
    {
        currencyEventChannel.OnMoneyUsed += OnMoneyUsed;
    }

    public override void Unload()
    {
        currencyEventChannel.OnMoneyUsed -= OnMoneyUsed;
    }

    private void OnMoneyUsed(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var money = (int)args["money"];

        CurrentAmount += money;
        Evaluate();
    }

}
