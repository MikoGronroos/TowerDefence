using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviourSingletonDontDestroyOnLoad<AccountManager>
{

    [SerializeField] private Account currentAccount;

    [SerializeField] private ServerEventChannel serverEventChannel;

    public Account CurrentAccount
    {
        get
        {
            return currentAccount;
        }
        set
        {
            currentAccount = value;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        serverEventChannel.OnPlayfabLogin += OnLoginListener;
    }

    private void OnDisable()
    {
        serverEventChannel.OnPlayfabLogin -= OnLoginListener;
    }

    private void OnLoginListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        LoadDataAccountData();
    }

    public void LoadDataAccountData()
    {
        LoadData.LoadDataFromPlayFab(()=> {
            serverEventChannel.OnAccountDataFetched?.Invoke();
        });
    }

    public void SaveDataAccountData()
    {
        SaveData.SaveTheAccountData(currentAccount);
    }

}
