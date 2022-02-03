using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/AddHardCurrency")]
public class EconomyCmdAddHCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        Debug.Log($"Added {amount} Hard Currency To PlayFab");

        PlayFabCurrencyManager.Instance.AddHardCurrency(amount);

        return true;

    }

}
