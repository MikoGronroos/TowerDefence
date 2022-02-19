using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Game/AddXpToPlayer")]
public class GameCmdAddXpToPlayer : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Added {amount} XP To LocalPlayer";

        PlayerLevel.Instance.AddXp(amount);

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }
}
