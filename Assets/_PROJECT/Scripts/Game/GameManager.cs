using Finark.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerEventChannel playerEventChannel;
    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    [SerializeField] private GameEndReward reward;

    [SerializeField] private bool gameEnded = false;

    private void OnEnable()
    {
        playerEventChannel.OnPlayerDead += EndGame;
    }

    private void OnDisable()
    {

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

        if (loserID != PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
        {
            AccountManager.Instance.CurrentAccount.Winstreak++;
        }
        else
        {
            AccountManager.Instance.CurrentAccount.Winstreak = 0;
        }

        sceneManagementEventChannel?.UnloadScenes(null, OnScenesUnloaded);

    }

    private void OnScenesUnloaded(Dictionary<string, object> obj)
    {
        PhotonNetwork.LoadLevel("GameEndScreen");
    }

    #endregion

}

public class GameEndReward : ScriptableObject
{
    public int SoftCurrencyReward;
    public int ExperienceReward;
}
