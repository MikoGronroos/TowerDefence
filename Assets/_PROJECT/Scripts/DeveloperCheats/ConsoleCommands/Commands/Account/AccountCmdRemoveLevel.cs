using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Account/RemoveLevel")]
public class AccountCmdRemoveLevel : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Removed {amount} Levels From Account Level.";

        AccountManager.Instance.CurrentAccount.AccountLevel -= amount;

        return true;
    }
}
