using Finark.Events;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    [SerializeField] private ServerEventChannel serverEventChannel;

    public override void OnEnable()
    {
        base.OnEnable();
        serverEventChannel.OnLogin += ConnectToLobby;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        serverEventChannel.OnLogin -= ConnectToLobby;
    }

    private void ConnectToLobby(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        serverEventChannel.OnLobbyJoined?.Invoke();
        SceneManager.LoadScene("Headquarter");
    }


}
