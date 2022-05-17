using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Finark.Events;
using System.Collections.Generic;
using System;

public class PlayFabLogin : MonoBehaviour
{

    [SerializeField] private UnityEvent onLoginEvent;

    [SerializeField] private ServerEventChannel serverEventChannel;

    private void OnEnable()
    {
        serverEventChannel.OnLobbyJoined += OnLobbyJoinedListener;
    }

    private void OnDisable()
    {
        serverEventChannel.OnLobbyJoined -= OnLobbyJoinedListener;
    }

    private void OnLobbyJoinedListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"PlayFab Login/Account Creation Error");
        Debug.Log($"{error.GenerateErrorReport()}");
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log($"{result.PlayFabId}: has logged in!");
        SceneManager.LoadScene("Headquarter");
        onLoginEvent?.Invoke();
    }
}
