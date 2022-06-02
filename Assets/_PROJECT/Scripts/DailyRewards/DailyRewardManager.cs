using Finark.Events;
using Finark.LootTable;
using System;
using UnityEngine;
using System.Collections.Generic;

public class DailyRewardManager : MonoBehaviourSingleton<DailyRewardManager>
{

    [SerializeField] private LootTable currentLootTable;

    [SerializeField] private HeadquartersEventChannel headquartersEventChannel;

    private void Start()
    {
        var claimTime = TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp.GetTimeInfoAsDateTime().AddHours(24);
        Debug.Log(claimTime);
        var n = DateTime.Compare(GetNistTime.GetNISTDate(), claimTime);
        bool availability = false;
        if (n > 0)
        {
            availability = true;
        }

        headquartersEventChannel.DailyRewardAvailability?.Invoke(new Dictionary<string, object> { { "IsAvailable", availability } });

    }

    public void ClaimDailyReward()
    {
        TimeManager.Instance.GetTimeData().ClaimedDailyRewardTimeStamp = new TimeInfo(GetNistTime.GetNISTDate());
        TimeManager.Instance.SaveTimeData();
    }

}
