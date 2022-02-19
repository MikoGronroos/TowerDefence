using Finark.Events;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{

    [SerializeField] private ServerEventChannel serverEventChannel;

    public override void OnEnable()
    {
        serverEventChannel.LeaveRoom += LeaveTheRoom;
    }

    public override void OnDisable()
    {
        serverEventChannel.LeaveRoom -= LeaveTheRoom;
    }

    private void LeaveTheRoom(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        StartCoroutine(LeavingTheRoom());

    }

    private IEnumerator LeavingTheRoom()
    {
        PhotonNetwork.LeaveRoom();

        while (PhotonNetwork.InRoom)
            yield return null;

        SceneManager.LoadScene("Headquarter");
    }

}
