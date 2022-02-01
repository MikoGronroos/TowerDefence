using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;

public class SaveAccountData
{

    public static void SaveData(Account data)
    {

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "AccountData", JsonUtility.ToJson(data) }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);

    }

    private static void OnError(PlayFabError error)
    {
        Debug.LogError($"PlayFab Account Data Sending Error");
        Debug.Log($"{error.GenerateErrorReport()}");
    }

    private static void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Data successfully sent");
    }
}
