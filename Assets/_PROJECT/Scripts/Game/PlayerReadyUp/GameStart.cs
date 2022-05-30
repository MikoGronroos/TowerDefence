using Finark.Events;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviourSingleton<GameStart>
{

    [SerializeField] private RoomEventChannel roomEventChannel;

    [SerializeField] private int currentlyReadyPlayers = 0;
    [SerializeField] private int playersInTheRoom = 0;

    private string _sceneToLoad;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        #region SpawnLocalPlayer

        LocalPlayer local = new LocalPlayer();

        int playerID;

        if (PhotonNetwork.IsMasterClient)
        {
            playerID = 0;
        }
        else
        {
            playerID = 1;
        }

        local.SetPlayerID(playerID);

        PlayerManager.Instance.AddLocalPlayer(local);

        #endregion

    }

    public override void OnEnable()
    {
        base.OnEnable();
        roomEventChannel.OnJoinedRoom += OnJoined;
        roomEventChannel.OnMapChanged += OnMapChangedListener;
    }

    private void OnDisable()
    {
        roomEventChannel.OnJoinedRoom -= OnJoined;
        roomEventChannel.OnMapChanged -= OnMapChangedListener;
    }

    private void OnJoined(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        _photonView.RPC("PlayerJoinedOrLeft", RpcTarget.AllBuffered, true);
    }


    #region Player Amounts

    private void IncreaseAmountOfPlayersInTheRoom(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        playersInTheRoom++;
        roomEventChannel.OnPlayerAmountChanged?.Invoke(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom } });
    }

    public void DecreaseAmountOfPlayersInTheRoom(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        playersInTheRoom--;
        roomEventChannel.OnPlayerAmountChanged?.Invoke(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom} });
    }

    public void IncreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers++;
        roomEventChannel?.OnPlayerAmountChanged(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom } });
        if (PhotonNetwork.IsMasterClient)
        {
            TryToStartTheGame();
        }
    }

    public void DecreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers--;
        roomEventChannel.OnPlayerAmountChanged?.Invoke(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom } });
    }

    #endregion

    private void OnMapChangedListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var map = (Map)args["Map"];

        _sceneToLoad = map.SceneName;

    }

    private void TryToStartTheGame()
    {
        if (currentlyReadyPlayers >= playersInTheRoom && playersInTheRoom > 1)
        {
            _photonView.RPC("RPCStartGame", RpcTarget.All);
        }
    }

    #region RPC Methods

    [PunRPC]
    private void PlayerJoinedOrLeft(bool value)
    {
        IncreaseAmountOfPlayersInTheRoom(null, null);
    }

    [PunRPC]
    private void RPCStartGame()
    {
        PhotonNetwork.LoadLevel(_sceneToLoad);
    }

    #endregion

}
