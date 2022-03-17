using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviourSingleton<PlayerLevel>
{

    [SerializeField] private int currentLevel;
    [SerializeField] private int currentXp;

    [SerializeField] private LevelXpRequirementChart levelXpRequirementChart;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void Start()
    {
        playerEventChannel?.OnPlayerXPChanged(new Dictionary<string, object> { 
            { "CurrentXP", currentXp }, 
            { "LevelXpReq", levelXpRequirementChart.XpRequirements[currentLevel] } 
        });
    }

    public void AddXp(int value)
    {
        if (value == 0) return;

        currentXp += value;

        var levelXpReq = levelXpRequirementChart.XpRequirements[currentLevel];

        if (currentXp >= levelXpReq)
        {

            currentXp = currentXp - levelXpReq;

            SetLevel(currentLevel + 1);
        }

        playerEventChannel?.OnPlayerXPChanged(new Dictionary<string, object> {
            { "CurrentXP", currentXp },
            { "LevelXpReq", levelXpRequirementChart.XpRequirements[currentLevel] }
        });

    }

    public void SetLevel(int value)
    {

        if (currentLevel >= levelXpRequirementChart.XpRequirements.Length) return;

        currentLevel = value;

        playerEventChannel?.OnPlayerLevelUp(new Dictionary<string, object> { { "CurrentLevel", currentLevel } });

    }


    public int GetCurrentLevel()
    {
        return currentLevel;
    }

}
