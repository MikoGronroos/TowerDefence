using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{

    [Header("Currency")]
    public int StartingCurrency;
    public int StartingIncome;

    public float IncomeInterval;

    [Header("Player Stats")]
    public int StartingHealth;
    public int StartingLevel;

    [Header("Pvp Missions")]
    public int StartingAmountOfMissions;


}
