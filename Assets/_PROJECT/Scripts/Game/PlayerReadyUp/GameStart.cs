using Finark.Events;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
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

        int amountOfPlayers = PhotonNetwork.PlayerList.Length - 1;

        local.IsLocal = true;

        local.SetPlayerID(amountOfPlayers);

        PlayerManager.Instance.AddLocalPlayer(local);

        #endregion

    }

    private void OnEnable()
    {
        roomEventChannel.OnJoinedRoom += OnJoined;
        roomEventChannel.OnPlayerReadyUp += LocalPlayerReadyUp;
        roomEventChannel.OnMapChanged += OnMapChangedListener;
    }

    private void OnDisable()
    {
        roomEventChannel.OnJoinedRoom -= OnJoined;
        roomEventChannel.OnPlayerReadyUp -= LocalPlayerReadyUp;
        roomEventChannel.OnMapChanged -= OnMapChangedListener;
    }

    public void LocalPlayerReadyUp(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var localPlayer = PlayerManager.Instance.GetLocalPlayer();

        if (!localPlayer.IsPlayerReady())
        {
            _photonView.RPC("RPCLocalPlayerReadyUp", RpcTarget.AllBuffered);
            localPlayer.TogglePlayerIsReady(true);
        }
    }

    #region Player Amounts

    private void OnJoined(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        _photonView.RPC("PlayerJoinedOrLeft", RpcTarget.AllBuffered, true);
    }

    private void OnLeft(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        _photonView.RPC("PlayerJoinedOrLeft", RpcTarget.AllBuffered, false);
    }

    private void IncreaseAmountOfPlayerInTheRoom(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        playersInTheRoom++;
        roomEventChannel.OnPlayerAmountChanged?.Invoke(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom } });
    }

    public void DecreaseAmountOfPlayerInTheRoom(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
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
            StartGame();
        }
    }

    public void DecreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers--;
        roomEventChannel.OnPlayerAmountChanged?.Invoke(new Dictionary<string, object> { { "CurrentlyReady", currentlyReadyPlayers }, { "PlayerInTheRoom", playersInTheRoom } });
    }

    public int GetAmountOfPlayersInTheRoom()
    {
        return playersInTheRoom;
    }

    #endregion

    private void OnMapChangedListener(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        var map = (Map)args["Map"];

        _sceneToLoad = map.SceneName;

    }

    private void StartGame()
    {
        if (currentlyReadyPlayers >= playersInTheRoom && playersInTheRoom > 1)
        {
            _photonView.RPC("RPCStartGame", RpcTarget.All);
        }

#if UNITY_EDITOR
        _photonView.RPC("RPCStartGame", RpcTarget.All);
#endif

    }

    #region RPC Methods

    [PunRPC]
    private void PlayerJoinedOrLeft(bool value)
    {
        IncreaseAmountOfPlayerInTheRoom(null, null);
    }

    [PunRPC]
    private void RPCStartGame()
    {
        PhotonNetwork.LoadLevel(_sceneToLoad);
    }

    [PunRPC]
    private void RPCLocalPlayerReadyUp()
    {
        IncreaseAmountOfReadyPlayers();
    }

    #endregion

}
