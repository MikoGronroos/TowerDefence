using Finark.Events;
using Photon.Pun;
using UnityEngine;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    [SerializeField] private ServerEventChannel serverEventChannel;

    private void Start()
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
    }


}
