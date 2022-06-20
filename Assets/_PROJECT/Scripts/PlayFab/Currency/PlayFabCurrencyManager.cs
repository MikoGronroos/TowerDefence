using Finark.Events;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabCurrencyManager : MonoBehaviourSingletonDontDestroyOnLoad<PlayFabCurrencyManager>
{

    [SerializeField] private int hardCurrency;
    [SerializeField] private int softCurrency;

    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;
    [SerializeField] private ServerEventChannel serverEventChannel;

    public override void OnEnable()
    {
        base.OnEnable();
        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency += HardCurrencyChanged;
        playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency += SoftCurrencyChanged;
        playFabCurrencyEventChannel.RefreshHardAndSoftCurrencies += RefreshCurrencies;
        serverEventChannel.OnPlayfabLogin += OnLoginListener;
    }

    private void OnDisable()
    {
        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency -= HardCurrencyChanged;
        playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency -= SoftCurrencyChanged;
        playFabCurrencyEventChannel.RefreshHardAndSoftCurrencies -= RefreshCurrencies; 
        serverEventChannel.OnPlayfabLogin -= OnLoginListener;
    }

    private void SoftCurrencyChanged(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int amount = (int)args["Amount"];

        if (amount > 0)
        {
            AddSoftCurrency(amount);
        }
        else
        {
            //Multiply amount with -1 to make the amount positive number.
            RemoveSoftCurrency(amount * -1);
        }
    }

    private void HardCurrencyChanged(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int amount = (int)args["Amount"];

        if (amount > 0)
        {
            AddHardCurrency(amount);
        }
        else
        {
            //Multiply amount with -1 to make the amount positive number.
            RemoveHardCurrency(amount * -1);
        }

    }

    #region Currency Modifying

    private void RemoveHardCurrency(int value)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HC",
            Amount = value
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubstractHCSuccess, OnSubstractHCError);
    }

    private void RemoveSoftCurrency(int value)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "SC",
            Amount = value
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubstractSCSuccess, OnSubstractSCError);
    }

    private void AddHardCurrency(int value)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "HC",
            Amount = value
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddHCSuccess, OnAddHCError);
    }

    private void AddSoftCurrency(int value)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "SC",
            Amount = value
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddSCSuccess, OnAddSCError);
    }

    #endregion

    #region Currency Checks

    public bool HasEnoughtSoftCurrency(int cost)
    {

        RefreshCurrencies(null, null);

        return softCurrency >= cost;
    }
    public bool HasEnoughtHardCurrency(int cost)
    {
        RefreshCurrencies(null, null);

        return hardCurrency >= cost;
    }


    #endregion

    private void RefreshCurrencies(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (GetUserInventoryResult result) => 
        {
            hardCurrency = result.VirtualCurrency["HC"];
            softCurrency = result.VirtualCurrency["SC"];
            playFabCurrencyEventChannel.AmountOfHardCurrencyChanged?.Invoke(new Dictionary<string, object> { { "TotalAmount", result.VirtualCurrency["HC"] } });
            playFabCurrencyEventChannel.AmountOfSoftCurrencyChanged?.Invoke(new Dictionary<string, object> { { "TotalAmount", result.VirtualCurrency["SC"] } });
            callback?.Invoke(null);
        }, 
        OnRefreshError);
    }

    #region PlayFab Callbacks

    private void OnRefreshError(PlayFabError error)
    {
    }

    #region Substract

    private void OnSubstractHCError(PlayFabError error)
    {
        Debug.Log("Substract hard currency error");
    }

    private void OnSubstractHCSuccess(ModifyUserVirtualCurrencyResult result)
    {
        RefreshCurrencies(null, null);
    }

    private void OnSubstractSCError(PlayFabError error)
    {
        Debug.Log("Substract soft currency error");
        Debug.Log(error.ErrorMessage);
    }

    private void OnSubstractSCSuccess(ModifyUserVirtualCurrencyResult result)
    {
        RefreshCurrencies(null,null);
    }

    #endregion

    #region Add

    private void OnAddSCError(PlayFabError obj)
    {
    }

    private void OnAddSCSuccess(ModifyUserVirtualCurrencyResult obj)
    {
        RefreshCurrencies(null, null);
    }

    private void OnAddHCError(PlayFabError obj)
    {
    }

    private void OnAddHCSuccess(ModifyUserVirtualCurrencyResult obj)
    {
        RefreshCurrencies(null, null);
    }

    #endregion

    #endregion

    private void OnLoginListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        RefreshCurrencies(null,null);
    }


}
