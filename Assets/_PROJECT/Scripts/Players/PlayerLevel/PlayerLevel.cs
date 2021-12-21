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

        _playerLevelUI.UpdateLevelText(currentLevel);
    }

    public void SetXP(int value)
    {
        currentXp = value;
        var levelXpReq = levelXpRequirementChart.XpRequirements[currentLevel];
        if (currentXp >= levelXpReq)
        {
            SetLevel(currentLevel + 1);
        }
        _playerLevelUI.UpdateXpProgressBar(currentXp, levelXpReq);
    }


}
