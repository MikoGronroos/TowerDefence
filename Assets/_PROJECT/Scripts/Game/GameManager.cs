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
    [SerializeField] private PlayFabCurrencyEventChannel playFabCurrencyEventChannel;

    [SerializeField] private GameRewards reward;

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
            playFabCurrencyEventChannel.ChangeAmountOfSoftCurrency?.Invoke(new Dictionary<string, object> { { "Amount", reward.SoftCurrencyReward } });
            AccountManager.Instance.CurrentAccount.AccountXp += reward.ExperienceReward;
        }
        else
        {
            AccountManager.Instance.CurrentAccount.Winstreak = 0;
        }

        AccountManager.Instance.SaveData();

        sceneManagementEventChannel?.UnloadScenes(null, OnScenesUnloaded);

    }

    private void OnScenesUnloaded(Dictionary<string, object> obj)
    {
        PhotonNetwork.LoadLevel("GameEndScreen");
    }

    #endregion

}