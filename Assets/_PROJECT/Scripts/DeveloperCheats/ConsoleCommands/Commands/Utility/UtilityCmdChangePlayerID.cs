using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Utility/ChangePlayerID")]
public class UtilityCmdChangePlayerID : BaseCommand
{
    public override bool Process(string[] args)
    {

        if (PlayerManager.Instance == null)
        {
            DeveloperConsoleController.Instance.PrintToConsole($"PlayerManager Instance was not found!!", PrintType.Error);
            return false;
        }

        int id = Int32.Parse(args[0]);

        DeveloperConsoleController.Instance.PrintToConsole($"Player ID has been changed to {id}!", PrintType.Success);

        PlayerManager.Instance.GetLocalPlayer().SetPlayerID(id);

        return true;
    }
}
