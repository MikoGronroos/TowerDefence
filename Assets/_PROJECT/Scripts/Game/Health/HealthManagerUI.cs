using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManagerUI : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> playerHealthTexts = new List<TextMeshProUGUI>();

    public void UpdateHealthText(Dictionary<string, object> args)
    {
        var health = (PlayerHealth)args["PlayerHealth"];

        var id = health.PlayerID;

        var healthText = playerHealthTexts[id];

        healthText.text = $"Health: {health.Health}";
    }
}
