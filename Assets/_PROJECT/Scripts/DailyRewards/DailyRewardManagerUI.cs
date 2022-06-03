using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyRewardManagerUI : MonoBehaviour
{

    [SerializeField] private Button claimDailyRewardButton;
    [SerializeField] private GameObject claimDailyRewardGameObject;

    [Header("Soft Currency Reward")]

    [SerializeField] private GameObject softCurrencyRewardPanel;
    [SerializeField] private TextMeshProUGUI softCurrencyRewardValueText;

    [Header("Hard Currency Reward")]

    [SerializeField] private GameObject hardCurrencyRewardPanel;
    [SerializeField] private TextMeshProUGUI hardCurrencyRewardValueText;

    [Header("Skin Reward")]

    [SerializeField] private GameObject skinRewardPanel;
    [SerializeField] private TextMeshProUGUI skinRewardValueText;

    [SerializeField] private HeadquartersEventChannel headquartersEventChannel;

    private void Awake()
    {
        claimDailyRewardButton.onClick.AddListener(()=> {
            DailyRewardManager.Instance.ClaimDailyReward();
            claimDailyRewardGameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        headquartersEventChannel.DailyRewardAvailability += DailyRewardAvailabilityListener;
    }

    private void OnDisable()
    {
        headquartersEventChannel.DailyRewardAvailability -= DailyRewardAvailabilityListener;
    }

    private void DailyRewardAvailabilityListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        bool availability = (bool)args["IsAvailable"];
        LootTableTierReward reward = (LootTableTierReward)args["Reward"];

        if (!availability)
        {
            claimDailyRewardGameObject.SetActive(false);
        }
        else
        {
            switch (reward.Type)
            {
                case RewardType.SoftCurrency:
                    softCurrencyRewardPanel.SetActive(true);
                    softCurrencyRewardValueText.text = reward.SoftCurrencyAmount.ToString();
                    break;
                case RewardType.HardCurrency:
                    hardCurrencyRewardPanel.SetActive(true);
                    hardCurrencyRewardValueText.text = reward.HardCurrencyAmount.ToString();
                    break;
                case RewardType.Skin:
                    skinRewardPanel.SetActive(true);
                    skinRewardValueText.text = reward.SkinGraphicMainKey;
                    break;
            }
        }

    }

}