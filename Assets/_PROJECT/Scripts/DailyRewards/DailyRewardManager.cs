using Finark.Events;
using Finark.LootTable;
using System;
using UnityEngine;
using System.Collections.Generic;

public class DailyRewardManager : MonoBehaviourSingleton<DailyRewardManager>
{

    [SerializeField] private LootTable currentLootTable;

    [SerializeField] private bool debugMode;

    [SerializeField] private HeadquartersEventChannel headquartersEventChannel;
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private LootTableTierReward _currentReward;

    private void Start()
    {
        bool availability = false;
        if (!TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp.IsEmpty())
        {

            DateTime claimTime;

            if (debugMode)
            {
                claimTime = TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp.GetTimeInfoAsDateTime();
            }
            else
            {
                claimTime = TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp.GetTimeInfoAsDateTime().AddHours(24);
            }

            var n = DateTime.Compare(GetNistTime.GetNISTDate(), claimTime);
            if (n > 0)
            {
                availability = true;
            }
        }
        else
        {
            availability = true;
        }

        _currentReward = currentLootTable.GetLootTableReward();

        headquartersEventChannel.DailyRewardAvailability?.Invoke(new Dictionary<string, object> { { "IsAvailable", availability }, { "Reward", _currentReward } });

    }

    public void ClaimDailyReward()
    {

        switch (_currentReward.Type)
        {
            case RewardType.SoftCurrency:
                playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", _currentReward.SoftCurrencyAmount } });
                break;
            case RewardType.HardCurrency:
                playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", _currentReward.HardCurrencyAmount } });
                break;
            case RewardType.Skin:
                break;
        }

        TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp = new TimeInfo(GetNistTime.GetNISTDate());
        TimeManager.Instance.SaveTimeData();
    }

}
