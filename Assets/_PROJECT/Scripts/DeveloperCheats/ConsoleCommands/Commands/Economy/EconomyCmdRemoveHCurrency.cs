using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/RemoveHardCurrency")]
public class EconomyCmdRemoveHCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Removed {amount} Hard Currency To PlayFab";

        PlayFabCurrencyManager.Instance.RemoveHardCurrency(amount);

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }

}