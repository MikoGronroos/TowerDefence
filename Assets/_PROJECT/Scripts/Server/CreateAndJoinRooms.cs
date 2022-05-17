using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    [SerializeField] private string sceneToLoad;

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomInput;

    [SerializeField] private bool _isCreatingOrJoiningARoom = false;

    public void CreateRoom()
    {
        if (!_isCreatingOrJoiningARoom)
        {

            if (createRoomInput.text == "") return;

            _isCreatingOrJoiningARoom = true;
            PhotonNetwork.CreateRoom(createRoomInput.text);
        }
    }

    public void JoinRoom()
    {
        if (!_isCreatingOrJoiningARoom)
        {

            if (joinRoomInput.text == "") return;

            _isCreatingOrJoiningARoom = true;
            PhotonNetwork.JoinRoom(joinRoomInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        _isCreatingOrJoiningARoom = false;
        PhotonNetwork.LoadLevel(sceneToLoad);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _isCreatingOrJoiningARoom = false;
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _isCreatingOrJoiningARoom = false;
        base.OnJoinRoomFailed(returnCode, message);
    }

} 
