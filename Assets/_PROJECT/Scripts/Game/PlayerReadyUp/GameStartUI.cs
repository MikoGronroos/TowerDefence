using Finark.Events;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amountOfPlayersReadyText;

    [SerializeField] private RoomEventChannel roomEventChannel;

    private void OnEnable()
    {
        roomEventChannel.OnPlayerAmountChanged += UpdateAmountOfPlayersText;
    }

    private void OnDisable()
    {
        roomEventChannel.OnPlayerAmountChanged -= UpdateAmountOfPlayersText;
    }

    public void UpdateAmountOfPlayersText(Dictionary<string, object> args, Action<Dictionary<string, object>> callback) 
    {
        int currentlyReady = (int)args["CurrentlyReady"]; 
        int playersInTheGame = (int)args["PlayerInTheRoom"];
        amountOfPlayersReadyText.text = $"{currentlyReady}/{playersInTheGame}"; 
    }

}
