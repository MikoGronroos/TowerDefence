using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Finark.Events;

public class HealthManagerUI : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> playerHealthTexts = new List<TextMeshProUGUI>();

    [SerializeField] private PlayerEventChannel playerEventChannel;

    private void OnEnable()
    {
        playerEventChannel.OnHealthChanged += UpdateHealthText;
    }

    private void OnDisable()
    {
        playerEventChannel.OnHealthChanged -= UpdateHealthText;
    }

    public void UpdateHealthText(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var health = (PlayerHealth)args["PlayerHealth"];

        var id = health.PlayerID;

        var healthText = playerHealthTexts[id];

        healthText.text = $"Health: {health.Health}";
    }
}
