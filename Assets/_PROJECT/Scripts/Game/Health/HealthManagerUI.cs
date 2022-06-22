using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Finark.Events;

public class HealthManagerUI : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> playerHealthTexts = new List<TextMeshProUGUI>();

    [SerializeField] private List<Slider> playerHealthBars = new List<Slider>();

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
        var id = (int)args["ID"];
        var amount = (int)args["amount"];

        var healthText = playerHealthTexts[id];
        var healthSlider = playerHealthBars[id];

        healthText.text = amount.ToString();
        healthSlider.value = (float)amount / 75;
    }
}
