using Finark.Events;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{

    [SerializeField] private RoomEventChannel roomEventChannel;

    public override void OnEnable()
    {
        roomEventChannel.LeaveRoom += LeaveTheRoom;
    }

    public override void OnDisable()
    {
        roomEventChannel.LeaveRoom -= LeaveTheRoom;
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
