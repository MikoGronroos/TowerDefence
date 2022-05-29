using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MatchmakingManager : MonoBehaviourPunCallbacks
{

	[SerializeField] private Button matchmakingButton;
    [SerializeField] private Button cancelMatchmakingButton;

    [SerializeField] private GameObject findMatchPanel;
    [SerializeField] private GameObject searchingMatchPanel;

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

        PhotonNetwork.JoinRandomRoom();
    }

    private void StopMatchmaking()
    {

        findMatchPanel.SetActive(true);
        searchingMatchPanel.SetActive(false);

        _isMatchmakingGame = false;

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
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room_" + randomRoomName, roomOptions);
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
