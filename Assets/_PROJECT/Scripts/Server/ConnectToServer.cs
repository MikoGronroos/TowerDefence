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
        serverEventChannel.OnAccountDataFetched += ConnectToLobby;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        serverEventChannel.OnAccountDataFetched -= ConnectToLobby;
    }

    private void ConnectToLobby(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        serverEventChannel.OnLobbyJoined?.Invoke();
        SceneManager.LoadScene("Headquarter");
    }


}
