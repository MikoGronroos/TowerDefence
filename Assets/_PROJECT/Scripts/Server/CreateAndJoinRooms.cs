using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

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

} 
