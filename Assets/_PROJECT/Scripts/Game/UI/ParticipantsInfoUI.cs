using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Finark.Events;

public class ParticipantsInfoUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI[] playerNames;

    [SerializeField] private TextMeshProUGUI[] playerTrophyAmounts;

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
        int trophyAmount = (int)args["Trophies"];

        playerNames[id].text = name;
        playerTrophyAmounts[id].text = trophyAmount.ToString();

    }

}
