using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

}
