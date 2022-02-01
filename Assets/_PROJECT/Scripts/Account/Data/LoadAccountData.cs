using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LoadAccountData
{

    public static void LoadData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    private static void OnError(PlayFabError error)
    {
    }

    private static void OnDataReceived(GetUserDataResult result)
    {

        Debug.Log($"Data Received Successfully");

        Account data = null;

        if (result.Data != null && result.Data.ContainsKey("AccountData"))
        {
            data = JsonUtility.FromJson<Account>(result.Data["AccountData"].Value);
            AccountManager.Instance.CurrentAccount = data;
        }
        else
        {
            Debug.Log($"Data Not Complete");
        }

    }

}
