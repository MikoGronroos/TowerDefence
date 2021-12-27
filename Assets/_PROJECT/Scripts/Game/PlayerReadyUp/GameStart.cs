using Photon.Pun;
using UnityEngine;

public class GameStart : MonoBehaviourSingleton<GameStart>
{

    [SerializeField] private string sceneToLoad;

    [SerializeField] private int currentlyReadyPlayers = 0;
    [SerializeField] private int playersInTheRoom = 0;

    private GameStartUI _gameStartUI;

    private PhotonView _photonView;

    private void Awake()
    {
        _gameStartUI = GetComponent<GameStartUI>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        #region SpawnLocalPlayer

        LocalPlayer local = new LocalPlayer();

        PlayerManager.Instance.AddLocalPlayer(local);

        int amountOfPlayers = PhotonNetwork.PlayerList.Length - 1;

        if (_photonView.IsMine)
        {
            local.IsLocal = true;
        }

        local.SetPlayerID(amountOfPlayers);

        #endregion


        _photonView.RPC("RPCValidatePlayer", RpcTarget.AllBuffered);
    }

    #region Player Amounts

    public void IncreaseAmountOfPlayerInTheRoom()
    {
        playersInTheRoom++;
        _gameStartUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public void DecreaseAmountOfPlayerInTheRoom()
    {
        playersInTheRoom--;
        _gameStartUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public void IncreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers++;
        _gameStartUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
        if (PhotonNetwork.IsMasterClient)
        {
            StartGame();
        }
    }

    public void DecreaseAmountOfReadyPlayers()
    {
        currentlyReadyPlayers--;
        _gameStartUI.UpdateAmountOfPlayersText(currentlyReadyPlayers, playersInTheRoom);
    }

    public int GetAmountOfPlayersInTheRoom()
    {
        return playersInTheRoom;
    }

    #endregion

    private void StartGame()
    {
        if (currentlyReadyPlayers >= playersInTheRoom)
        {
            _photonView.RPC("RPCStartGame", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPCValidatePlayer()
    {
        GameStart.Instance.IncreaseAmountOfPlayerInTheRoom();
    }

    [PunRPC]
    private void RPCStartGame()
    {
        PhotonNetwork.LoadLevel(sceneToLoad);
    }

}
