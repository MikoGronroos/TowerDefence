using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
