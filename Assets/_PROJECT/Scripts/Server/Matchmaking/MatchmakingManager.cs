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

        TypedLobby sqlLobby = new TypedLobby("rankedLobby", LobbyType.SqlLobby);
        string sqlFilter = $"C0 BETWEEN {AccountManager.Instance.CurrentAccount.CurrentTrophies - settings.MaxTrophyDifference} AND {AccountManager.Instance.CurrentAccount.CurrentTrophies + settings.MaxTrophyDifference}";
        PhotonNetwork.JoinRandomRoom(null, 2, MatchmakingMode.FillRoom, sqlLobby, sqlFilter, null);
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
            Debug.Log("Couldn't find a room - creating a room!");
            MakeRoom();
        }
    }

    private void MakeRoom()
    {
        int randomRoomName = Random.Range(0,5000);

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

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Starting The Game!");
                PhotonNetwork.LoadLevel("RoomView");
            }
        }
    }

}


