using Finark.Events;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUp : MonoBehaviour
{
	[SerializeField] private RoomEventChannel roomEventChannel;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        roomEventChannel.OnPlayerReadyUp += LocalPlayerReadyUp;
    }

    private void OnDisable()
    {
        roomEventChannel.OnPlayerReadyUp -= LocalPlayerReadyUp;
    }

    public void LocalPlayerReadyUp(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var localPlayer = PlayerManager.Instance.GetLocalPlayer();

        if (!localPlayer.IsPlayerReady())
        {
            _photonView.RPC("RPCLocalPlayerReadyUp", RpcTarget.All);
            localPlayer.TogglePlayerIsReady(true);
        }
        else
        {
            _photonView.RPC("RPCLocalPlayerNotReady", RpcTarget.All);
            localPlayer.TogglePlayerIsReady(false);
        }
    }

    #region PunRpc Methods

    [PunRPC]
    private void RPCLocalPlayerReadyUp()
    {
        GameStart.Instance.IncreaseAmountOfReadyPlayers();
    }

    [PunRPC]
    private void RPCLocalPlayerNotReady()
    {
        GameStart.Instance.DecreaseAmountOfReadyPlayers();
    }

    #endregion

}
