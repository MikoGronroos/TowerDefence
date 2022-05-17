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
        serverEventChannel.OnLogin += OnLoginListener;
    }

    private void OnDisable()
    {
        serverEventChannel.OnLogin -= OnLoginListener;
    }

    private void OnLoginListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        SaveData();
    }

    public void LoadData()
    {
        LoadAccountData.LoadData();
    }

    public void SaveData()
    {
        SaveAccountData.SaveTheAccountData(currentAccount);
    }

}
