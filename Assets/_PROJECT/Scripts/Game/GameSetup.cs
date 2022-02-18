using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{

    private void Start()
    {

        PlayerLevel.Instance.SetLevel(GameSettingsManager.Instance.GetGameSettings().StartingLevel);

        VirtualCurrencyManager.Instance.SetCurrency(GameSettingsManager.Instance.GetGameSettings().StartingCurrency);
        VirtualCurrencyManager.Instance.SetIncome(GameSettingsManager.Instance.GetGameSettings().StartingIncome);
        VirtualCurrencyManager.Instance.SetInterval(GameSettingsManager.Instance.GetGameSettings().IncomeInterval);

        PvpMissionManager.Instance.GetNewMissions(GameSettingsManager.Instance.GetGameSettings().StartingAmountOfMissions);

        HealthManager.Instance.SetHealhtOfEveryPlayer(GameSettingsManager.Instance.GetGameSettings().StartingHealth);

    }
}
