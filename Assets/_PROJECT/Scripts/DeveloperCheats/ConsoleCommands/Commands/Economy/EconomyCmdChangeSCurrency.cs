using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/ChangeSoftCurrency")]
public class EconomyCmdChangeSCurrency : BaseCommand
{
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Changed Soft Currency {amount} In PlayFab.";

        playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", amount } });

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }
}
