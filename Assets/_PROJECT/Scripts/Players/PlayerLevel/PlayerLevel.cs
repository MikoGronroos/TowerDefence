using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviourSingleton<PlayerLevel>
{

    [SerializeField] private int currentLevel;
    [SerializeField] private int currentXp;

    [SerializeField] private LevelXpRequirementChart levelXpRequirementChart;

    private PlayerLevelUI _playerLevelUI;

    public static Action OnLevelUpEvent;

    private void Awake()
    {
        _playerLevelUI = GetComponent<PlayerLevelUI>();
    }

    public void OnLevelUp()
    {

        currentXp = currentXp - levelXpRequirementChart.XpRequirements[currentLevel];

        currentLevel++;

        _playerLevelUI.UpdateLevelText(currentLevel);

        OnLevelUpEvent?.Invoke();

    }

    public void AddXp(int value)
    {
        currentXp += value;
        var levelXpReq = levelXpRequirementChart.XpRequirements[currentLevel];
        if (currentXp >= levelXpReq)
        {
            OnLevelUp();
        }
        _playerLevelUI.UpdateXpProgressBar(currentXp, levelXpReq);
    }

}
