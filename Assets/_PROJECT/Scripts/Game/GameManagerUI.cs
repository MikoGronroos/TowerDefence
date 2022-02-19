using Finark.Events;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerUI : MonoBehaviour
{

    [SerializeField] private PlayerEventChannel playerEventChannel;

    [Header("Game End Panel")]

    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Button leaveTheRoomButton;

    private void Awake()
    {
        leaveTheRoomButton.onClick.AddListener(() => {
            //GameManager.Instance.LeaveRoom();
        });
    }

    private void OnEnable()
    {
        playerEventChannel.OnPlayerDead += GameEndedUIToggle;
    }

    private void OnDisable()
    {
        playerEventChannel.OnPlayerDead -= GameEndedUIToggle;
    }

    private void GameEndedUIToggle(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        int loserID = (int)args["loserID"];

        string gameEndText;

        if (PlayerManager.Instance.GetLocalPlayer().GetPlayerID() != loserID)
        {
            gameEndText = "You won";
        }
        else
        {
            gameEndText = "You lost";
        }
        winnerText.text = gameEndText;
    }

}
