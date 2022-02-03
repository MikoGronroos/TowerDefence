using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/AddHardCurrency")]
public class EconomyCmdAddHCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Added {amount} Hard Currency To PlayFab";

        PlayFabCurrencyManager.Instance.AddHardCurrency(amount);

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }

}
