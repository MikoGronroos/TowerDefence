using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/AddSoftCurrency")]
public class EconomyCmdAddSCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        Debug.Log($"Added {amount} Soft Currency To PlayFab");

        PlayFabCurrencyManager.Instance.AddSoftCurrency(amount);

        return true;

    }
}
