using Finark.Events;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabCurrencyManager : MonoBehaviourSingleton<PlayFabCurrencyManager>
{

    [SerializeField] private int hardCurrency;
    [SerializeField] private int softCurrency;

    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    private PlayFabCurrencyUI _playFabCurrencyUI;

    private void Awake()
    {
        _playFabCurrencyUI = GetComponent<PlayFabCurrencyUI>();
    }

    private void OnEnable()
    {
        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency += HardCurrencyChanged;
        playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency += SoftCurrencyChanged;
    }

    private void OnDisable()
    {
        playFabCurrencyEventChannel.ChangeAmountOfHardCurrency -= HardCurrencyChanged;
        playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency -= SoftCurrencyChanged;
    }

    private void Start()
    {
        RefreshCurrencies();
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
        _playFabCurrencyUI.SetHardCurrencyText(hardCurrency.ToString());
        _playFabCurrencyUI.SetSoftCurrencyText(softCurrency.ToString());
    }

    #region Substract

    private void OnSubstractHCError(PlayFabError error)
    {
        Debug.Log("Substract hard currency error");
    }

    private void OnSubstractHCSuccess(ModifyUserVirtualCurrencyResult result)
    {
        RefreshCurrencies();
    }

    private void OnSubstractSCError(PlayFabError error)
    {
        Debug.Log("Substract soft currency error");
        Debug.Log(error.ErrorMessage);
    }

    private void OnSubstractSCSuccess(ModifyUserVirtualCurrencyResult result)
    {
        RefreshCurrencies();
    }

    #endregion

    #region Add

    private void OnAddSCError(PlayFabError obj)
    {
    }

    private void OnAddSCSuccess(ModifyUserVirtualCurrencyResult obj)
    {
        RefreshCurrencies();
    }

    private void OnAddHCError(PlayFabError obj)
    {
    }

    private void OnAddHCSuccess(ModifyUserVirtualCurrencyResult obj)
    {
        RefreshCurrencies();
    }

    #endregion

    #endregion

}
