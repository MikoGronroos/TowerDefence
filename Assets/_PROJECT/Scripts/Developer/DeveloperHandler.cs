using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperHandler : MonoBehaviour
{

    [SerializeField] private ServerEventChannel serverEventChannel;

    private void OnEnable()
    {
        serverEventChannel.OnLogin += OnLoginListener;
    }

        private void OnDisable()
    {
        serverEventChannel.OnLogin -= OnLoginListener;
    }

    private void OnLoginListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        CheckIfDeveloper();
    }

    public void CheckIfDeveloper()
    {
#if UNITY_EDITOR

        Debug.Log("Loading Developer Console!");

        SceneManager.LoadScene("DeveloperConsole", LoadSceneMode.Additive);

#endif
    }

}
