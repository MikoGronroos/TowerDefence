using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Utility/Help")]
public class UtilityCmdHelp : BaseCommand
{
    public override bool Process(string[] args)
    {

        string commandCategory = args[0];

        return true;

    }
}
