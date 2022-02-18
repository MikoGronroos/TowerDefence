using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviourSingleton<PlayerLevel>
{

    [SerializeField] private int currentLevel;
    [SerializeField] private int currentXp;

    [SerializeField] private LevelXpRequirementChart levelXpRequirementChart;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    public static Action OnLevelUpEvent;

    private void Start()
    {
        playerEventChannel.OnPlayerXPChanged(new Dictionary<string, object> { 
            { "CurrentXP", currentXp }, 
            { "LevelXpReq", levelXpRequirementChart.XpRequirements[currentLevel] } 
        });
    }

    public void OnLevelUp()
    {
        OnLevelUpEvent?.Invoke();
    }

    public void AddXp(int value)
    {
        var xp = currentXp += value;
        SetXP(xp);
    }

    public void SetLevel(int value)
    {

        currentLevel = value;
        OnLevelUp();

        playerEventChannel.OnPlayerLevelChanged(new Dictionary<string, object> { { "CurrentLevel", currentLevel } });

        SetXP(currentXp > 0 ? currentXp - levelXpRequirementChart.XpRequirements[currentLevel] : 0);

    }

    public void SetXP(int value)
    {

        if (value == 0) return;

        currentXp = value;
        var levelXpReq = levelXpRequirementChart.XpRequirements[currentLevel];

        playerEventChannel.OnPlayerXPChanged(new Dictionary<string, object> { 
            { "CurrentXP", currentXp }, 
            { "LevelXpReq", levelXpRequirementChart.XpRequirements[currentLevel] } 
        });

        if (currentXp >= levelXpReq)
        {
            SetLevel(currentLevel + 1);
        }
    }


    public int GetCurrentLevel()
    {
        return currentLevel;
    }

}
