using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Finark.Events;

public class ParticipantsInfoUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI[] playerNames;

    [SerializeField] private RoomEventChannel roomEventChannel;

    private void OnEnable()
    {
        roomEventChannel.OnPlayerInfoUpdate += SetPlayerInformationWithID;
    }

    private void OnDisable()
    {
        roomEventChannel.OnPlayerInfoUpdate -= SetPlayerInformationWithID;
    }

    private void SetPlayerInformationWithID(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int id = (int)args["ID"];

        string name = (string)args["Name"];

        playerNames[id].text = name;

    }

}
