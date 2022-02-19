using Finark.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerEventChannel playerEventChannel;

    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    [SerializeField] private bool gameEnded = false;

    private void OnEnable()
    {
        sceneManagementEventChannel.GameEndSceneLoaded += GameEndSceneLoaded;
        playerEventChannel.OnPlayerDead += EndGame;
    }



    private void OnDisable()
    {
        sceneManagementEventChannel.GameEndSceneLoaded -= GameEndSceneLoaded;
        playerEventChannel.OnPlayerDead -= EndGame;
    }

    public void StartChildCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    #region Game End

    private void EndGame(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        if (gameEnded) return;

        gameEnded = true;

        int loserID = (int)args["loserID"];

        SceneManager.LoadScene("GameEndScreen", LoadSceneMode.Additive);

        if (loserID != PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
        {
            AccountManager.Instance.CurrentAccount.Winstreak++;
        }
        else
        {
            AccountManager.Instance.CurrentAccount.Winstreak = 0;
        }

    }

    private void GameEndSceneLoaded(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        sceneManagementEventChannel?.UnloadScenes(null);
    }

    private void LeaveRoom()
    {
        RoomController.LeaveTheRoom();
    }

    #endregion

}
