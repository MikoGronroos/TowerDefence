using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NameChanger : MonoBehaviour
{

	[SerializeField] private AccountEventChannel accountEventChannel;

    private void OnEnable()
    {
        accountEventChannel.OnNameChanged += NameChanged;
    }

    private void OnDisable()
    {
        accountEventChannel.OnNameChanged -= NameChanged;
    }


    private void NameChanged(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        AccountManager.Instance.CurrentAccount.AccountName = (string)args["Name"];
        AccountManager.Instance.SaveData();
    }

}
