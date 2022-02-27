using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/ChangeHardCurrency")]
public class EconomyCmdChangeHCurrency : BaseCommand
{
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Changed Hard Currency {amount} In PlayFab.";

        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency?.Invoke(new Dictionary<string, object> { { "Amount", amount } });

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }
}