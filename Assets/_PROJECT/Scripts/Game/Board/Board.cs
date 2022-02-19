using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private int playerID;

    [SerializeField] private Path thisPath;

    [SerializeField] private PathEventChannel pathEventChannel;

    private void OnEnable()
    {
        pathEventChannel.SetupPaths += OnSetupPathListened;
    }

    private void OnDisable()
    {
        pathEventChannel.SetupPaths -= OnSetupPathListened;
    }

    private void OnSetupPathListened(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        callback.Invoke(new Dictionary<string, object> { { "Path", thisPath } });
    }

    public int GetID()
    {
        return playerID;
    }



}
