using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerLevelUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Image xpProgressBar;

    public void UpdateXpProgressBar(int currentXp, int maxXp)
    {
        xpProgressBar.fillAmount = (float)currentXp / (float)maxXp;
    }

    public void UpdateLevelText(int value)
    {
        levelText.text = $"{value}";
    }

    public void PlayerLevelUpChannelListener(Dictionary<string, object> args)
    {

        int currentLevel = (int)args["CurrentLevel"];

        UpdateLevelText(currentLevel);

    }

    public void PlayerXpAddonChannelListener(Dictionary<string, object> args)
    {

        int currentXp = (int)args["CurrentXP"];
        int requiredXp = (int)args["LevelXpReq"];

        UpdateXpProgressBar(currentXp, requiredXp);
    }

}
