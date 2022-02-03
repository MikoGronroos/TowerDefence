using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/RemoveSoftCurrency")]
public class EconomyCmdRemoveSCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Removed {amount} Soft Currency To PlayFab";

        PlayFabCurrencyManager.Instance.RemoveSoftCurrency(amount);

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }
}