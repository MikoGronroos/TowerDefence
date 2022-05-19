using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Turret Upgrade Paths")]
public class UpgradePaths : ScriptableObject
{

    private const int amountOfPaths = 3;

    public UpgradePath[] Paths = new UpgradePath[amountOfPaths];

    private void OnValidate()
    {
        if (Paths.Length != amountOfPaths)
        {
            Debug.LogWarning("Don't change the 'Paths' field's array size!");
            Array.Resize(ref Paths, amountOfPaths);
        }
    }

}