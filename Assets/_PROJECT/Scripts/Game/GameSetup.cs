using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;

public class GameSetup : MonoBehaviour
{

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {

        PlayerLevel.Instance.SetLevel(GameSettingsManager.Instance.GetGameSettings().StartingLevel);

        VirtualCurrencyManager.Instance.SetCurrency(GameSettingsManager.Instance.GetGameSettings().StartingCurrency);
        VirtualCurrencyManager.Instance.SetIncome(GameSettingsManager.Instance.GetGameSettings().StartingIncome);
        VirtualCurrencyManager.Instance.SetInterval(GameSettingsManager.Instance.GetGameSettings().IncomeInterval);

        HealthManager.Instance.SetHealhtOfEveryPlayer(GameSettingsManager.Instance.GetGameSettings().StartingHealth);

        PvpMissionManager.Instance.GetNewMissions(GameSettingsManager.Instance.GetGameSettings().StartingAmountOfMissions);

    }
}
