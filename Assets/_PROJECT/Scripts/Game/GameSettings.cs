using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{

    [Header("Currency")]
    public int StartingCurrency;
    public int StartingIncome;

    [Header("Player Stats")]
    public int StartingHealth;


}
