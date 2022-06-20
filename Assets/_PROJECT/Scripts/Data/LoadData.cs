using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public static class LoadData
{

    private static Action _callback;

    public static void LoadDataFromPlayFab(Action callback)
    {
        _callback = callback;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    private static void OnError(PlayFabError error)
    {
    }

    private static void OnDataReceived(GetUserDataResult result)
    {

        PlayerSkins graphicsData = null;
        Account accountData = null;
        TimeData timeData = null;

        if (result.Data != null && result.Data.ContainsKey("AccountData"))
        {
            accountData = JsonUtility.FromJson<Account>(result.Data["AccountData"].Value);
            AccountManager.Instance.CurrentAccount = accountData;
        }
        else
        {
            Debug.Log($"Creating Account Data For The First Time.");
            accountData = new Account();
            AccountManager.Instance.CurrentAccount = accountData;
        }

        if (result.Data != null && result.Data.ContainsKey("SkinManagerData"))
        {
            graphicsData = JsonUtility.FromJson<PlayerSkins>(result.Data["SkinManagerData"].Value);
            SkinManager.Instance.ReplaceGraphicsDataSet(graphicsData);
        }
        else
        {
            SaveData.SaveTheSkinManagerData(SkinManager.Instance.GetGraphicsData());
        }

        if (result.Data != null && result.Data.ContainsKey("TimeManagerData"))
        {
            timeData = JsonUtility.FromJson<TimeData>(result.Data["TimeManagerData"].Value);
            TimeManager.Instance.SetTimeData(timeData);
        }
        _callback?.Invoke();
    }

}
