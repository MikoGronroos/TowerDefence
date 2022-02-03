using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Account/AddLevel")]
public class AccountCmdAddLevel : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Added {amount} Levels To Account Level.";

        AccountManager.Instance.CurrentAccount.AccountLevel += amount;

        return true;
    }
}
