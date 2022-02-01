using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public class PlayFabCurrencyManager : MonoBehaviourSingletonDontDestroyOnLoad<PlayFabCurrencyManager>
{

    [SerializeField] private int hardCurrency;
    [SerializeField] private int softCurrency;

    public int GetHardCurrency()
    {
        RefreshCurrencies();
        return hardCurrency;
    }

    public int GetSoftCurrency()
    {
        RefreshCurrencies();
        return softCurrency;
    }

    public void RemoveHardCurrency(int value)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HC",
            Amount = value
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubstractHCSuccess, OnSubstractHCError);
    }

    public void RemoveSoftCurrency(int value)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "SC",
            Amount = value
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubstractSCSuccess, OnSubstractSCError);
    }

    private void RefreshCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnRefreshSuccess, OnRefreshError);
    }

    #region PlayFab Callbacks

    private void OnRefreshError(PlayFabError error)
    {
    }

    private void OnRefreshSuccess(GetUserInventoryResult result)
    {
        hardCurrency = result.VirtualCurrency["HC"];
        softCurrency = result.VirtualCurrency["SC"];
    }

    private void OnSubstractHCError(PlayFabError error)
    {
    }

    private void OnSubstractHCSuccess(ModifyUserVirtualCurrencyResult result)
    {
    }

    private void OnSubstractSCError(PlayFabError error)
    {
    }

    private void OnSubstractSCSuccess(ModifyUserVirtualCurrencyResult result)
    {
    }

    #endregion

}
