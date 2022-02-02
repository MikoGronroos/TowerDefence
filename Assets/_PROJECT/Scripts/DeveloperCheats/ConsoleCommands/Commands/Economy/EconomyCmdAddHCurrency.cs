using UnityEngine;

[CreateAssetMenu(menuName = "ConsoleCommands/Economy/AddHardCurrency")]
public class EconomyCmdAddHCurrency : BaseCommand
{
    public override bool Process(string[] args)
    {
        Debug.Log($"Added {args[1]} Hard Currency To PlayFab");

        return true;

    }

}
