using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Game/SetPlayerLevel")]
public class GameCmdSetPlayerLevel : BaseCommand
{
    public override bool Process(string[] args)
    {

        int amount = Int32.Parse(args[0]);

        string print = $"Set player level to {amount}.";

        PlayerLevel.Instance.SetLevel(amount);

        DeveloperConsoleController.Instance.PrintToConsole(print, PrintType.Success);

        return true;

    }

}