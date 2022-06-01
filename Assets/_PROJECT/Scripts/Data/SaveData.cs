using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{

    public static void SaveTheAccountData(Account data)
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

    public static void SaveTheSkinManagerData(PlayerSkins data)
    {

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "SkinManagerData", JsonUtility.ToJson(data) }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);

    }

    private static void OnError(PlayFabError error)
    {
        Debug.LogError($"PlayFab Data Saving Error");
        Debug.Log($"{error.GenerateErrorReport()}");
    }

    private static void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Data successfully sent");
    }
}
