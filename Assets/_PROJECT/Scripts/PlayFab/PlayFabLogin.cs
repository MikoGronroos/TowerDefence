using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{

    [SerializeField] private UnityEvent onLoginEvent;

    private void Start()
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
