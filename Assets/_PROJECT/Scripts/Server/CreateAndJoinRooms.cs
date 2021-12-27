using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    [SerializeField] private string sceneToLoad;

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomInput;

    private bool _isCreatingOrJoiningARoom = false;

    public void CreateRoom()
    {
        if (!_isCreatingOrJoiningARoom)
        {
            _isCreatingOrJoiningARoom = true;
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void JoinRoom()
    {
        if (!_isCreatingOrJoiningARoom)
        {
            _isCreatingOrJoiningARoom = true;
            PhotonNetwork.JoinRoom(joinRoomInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        _isCreatingOrJoiningARoom = false;
        PhotonNetwork.LoadLevel(sceneToLoad);
    }

} 
