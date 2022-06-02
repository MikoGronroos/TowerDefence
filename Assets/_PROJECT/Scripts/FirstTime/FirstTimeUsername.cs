using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Finark.Events;
using System.Collections.Generic;
using System;

public class FirstTimeUsername : MonoBehaviour
{

    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private Button usernameSubmitButton;
    [SerializeField] private GameObject usernameSetPanel;

    [SerializeField] private HeadquartersEventChannel headquartersEventChannel;

    private void Awake()
    {
        usernameSubmitButton.onClick.AddListener(()=> {
            SubmitUsername();
        });
    }

    private void OnEnable()
    {
        headquartersEventChannel.OnFirstTimeGameLoaded += OnFirstTimeGameLoadedListener;
    }

    private void OnDisable()
    {
        headquartersEventChannel.OnFirstTimeGameLoaded -= OnFirstTimeGameLoadedListener;
    }

    private void OnFirstTimeGameLoadedListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        usernameSetPanel.SetActive(true);
    }

    public void SubmitUsername()
    {
        if (usernameInputField.text != "")
        {
            AccountManager.Instance.CurrentAccount.AccountName = usernameInputField.text;
            AccountManager.Instance.CurrentAccount.IsFirstLaunchOfTheGame = false;
            usernameSetPanel.SetActive(false);
            AccountManager.Instance.SaveDataAccountData();
        }
    }

}
