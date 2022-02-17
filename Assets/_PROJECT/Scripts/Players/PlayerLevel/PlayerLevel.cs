using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviourSingleton<PlayerLevel>
{

    [SerializeField] private int currentLevel;
    [SerializeField] private int currentXp;

    [SerializeField] private LevelXpRequirementChart levelXpRequirementChart;

    [SerializeField] private EventChannel playerLevelUpChannel;
    [SerializeField] private EventChannel playerXpAddonChannel;

    public static Action OnLevelUpEvent;

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

        SetXP(currentXp > 0 ? currentXp - levelXpRequirementChart.XpRequirements[currentLevel] : 0);

        currentLevel = value;
        OnLevelUp();

        playerLevelUpChannel.RaiseEvent(new Dictionary<string, object> { { "CurrentLevel", currentLevel } });

    }

    public void SetXP(int value)
    {
        currentXp = value;
        var levelXpReq = levelXpRequirementChart.XpRequirements[currentLevel];
        if (currentXp >= levelXpReq)
        {
            SetLevel(currentLevel + 1);
        }
        playerXpAddonChannel.RaiseEvent(new Dictionary<string, object> { { "CurrentXP", currentXp }, { "LevelXpReq", levelXpReq } });
    }


    public int GetCurrentLevel()
    {
        return currentLevel;
    }

}
