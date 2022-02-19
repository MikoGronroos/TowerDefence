using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Finark.Events;

public class PlayerLevelUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Image xpProgressBar;

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.OnPlayerLevelChanged += UpdateLevelText;
        playerEventChannel.OnPlayerXPChanged += UpdateXpProgressBar;
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerLevelChanged -= UpdateLevelText;
        playerEventChannel.OnPlayerXPChanged -= UpdateXpProgressBar;
    }

    private void UpdateXpProgressBar(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        int currentXp = (int)args["CurrentXP"];
        int maxXp = (int)args["LevelXpReq"];

        xpProgressBar.fillAmount = (float)currentXp / (float)maxXp;
    }

    private void UpdateLevelText(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int currentLevel = (int)args["CurrentLevel"];

        levelText.text = $"{currentLevel}";
    }
}
