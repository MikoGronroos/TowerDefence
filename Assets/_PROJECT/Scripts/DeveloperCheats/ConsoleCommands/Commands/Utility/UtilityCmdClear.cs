using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Utility/Clear")]
public class UtilityCmdClear : BaseCommand
{
    public override bool Process(string[] args)
    {

        DeveloperConsoleController.Instance.ClearConsole();

        return true;

    }
}
