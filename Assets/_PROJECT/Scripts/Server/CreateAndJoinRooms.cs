using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomInput;

    private bool _isCreatingARoom = false;

    public void CreateRoom()
    {
        if (!_isCreatingARoom)
        {
            _isCreatingARoom = true;
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        _isCreatingARoom = false;
        PhotonNetwork.LoadLevel("Game");
    }

} 
