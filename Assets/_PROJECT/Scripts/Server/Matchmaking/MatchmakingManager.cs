using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{

	[SerializeField] private Button matchmakingButton;
    [SerializeField] private Button cancelMatchmakingButton;

    [SerializeField] private GameObject findMatchPanel;
    [SerializeField] private GameObject searchingMatchPanel;

    [SerializeField] private MatchmakingSettings settings;

    private bool _isMatchmakingGame = false;
    private int _currentJoinAttempt = 0;

    private void Awake()
    {
        matchmakingButton.onClick.AddListener(() => {
            StartMatchmaking();
        });
        cancelMatchmakingButton.onClick.AddListener(() => {
            StopMatchmaking();
        });
    }

    private void StartMatchmaking()
    {

        searchingMatchPanel.SetActive(true);
        findMatchPanel.SetActive(false);

        _isMatchmakingGame = true;

        JoinRoom(AccountManager.Instance.CurrentAccount.CurrentTrophies - settings.MaxTrophyDifference, AccountManager.Instance.CurrentAccount.CurrentTrophies + settings.MaxTrophyDifference);

    }

    private void StopMatchmaking()
    {

        findMatchPanel.SetActive(true);
        searchingMatchPanel.SetActive(false);

        _isMatchmakingGame = false;

        Debug.Log("Stopped matchmaking and left the room!");

        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (_isMatchmakingGame)
        {
            if (_currentJoinAttempt <= 2)
            {
                Debug.Log($"{_currentJoinAttempt} attemps to find a room!");
                int lowElo = AccountManager.Instance.CurrentAccount.CurrentTrophies - (settings.MaxTrophyDifference * (_currentJoinAttempt + 1));
                int highElo = AccountManager.Instance.CurrentAccount.CurrentTrophies + (settings.MaxTrophyDifference * (_currentJoinAttempt + 1));
                JoinRoom(lowElo, highElo);
                return;
            }
            Debug.Log("Couldn't find a room - creating a room!");
            MakeRoom();
        }
    }

    private void JoinRoom(int lowElo, int highElo)
    {
        _currentJoinAttempt++;
        TypedLobby sqlLobby = new TypedLobby("rankedLobby", LobbyType.SqlLobby);
        string sqlFilter = $"C0 BETWEEN {lowElo} AND {highElo}";
        PhotonNetwork.JoinRandomRoom(null, (byte)settings.MaxPlayers, MatchmakingMode.FillRoom, sqlLobby, sqlFilter, null);
    }

    private void MakeRoom()
    {

        _currentJoinAttempt = 0;

        int randomRoomName = Random.Range(0,50000);

        RoomOptions roomOptions = new RoomOptions() { IsVisible = settings.Public, IsOpen = true, MaxPlayers = (byte)settings.MaxPlayers };
        roomOptions.CustomRoomProperties = new Hashtable() { { "C0", AccountManager.Instance.CurrentAccount.CurrentTrophies } };
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "C0" };
        TypedLobby sqlLobby = new TypedLobby("rankedLobby", LobbyType.SqlLobby);
        PhotonNetwork.CreateRoom("Room_" + randomRoomName, roomOptions, sqlLobby);

        Debug.Log($"Created A Room: Room_{randomRoomName}.");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    { 
        if (_isMatchmakingGame)
        {

            _currentJoinAttempt = 0;

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting The Game!");
                PhotonNetwork.LoadLevel("RankedRoomLobby");
            }
        }
    }

}


