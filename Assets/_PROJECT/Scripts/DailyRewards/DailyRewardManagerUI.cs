using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardManagerUI : MonoBehaviour
{

    [SerializeField] private Button claimDailyRewardButton;
    [SerializeField] private GameObject claimDailyRewardGameObject;


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

        if (!availability)
        {
            claimDailyRewardGameObject.SetActive(false);
        }

    }

}