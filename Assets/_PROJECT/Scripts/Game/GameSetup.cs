using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        EventCreator.CreateAllEvents();

        VirtualCurrencyManager.Instance.SetCurrency(GameSettingsManager.Instance.GetGameSettings().StartingCurrency);
        VirtualCurrencyManager.Instance.SetIncome(GameSettingsManager.Instance.GetGameSettings().StartingIncome);
        VirtualCurrencyManager.Instance.SetInterval(GameSettingsManager.Instance.GetGameSettings().IncomeInterval);

        HealthManager.Instance.SetHealhtOfEveryPlayer(GameSettingsManager.Instance.GetGameSettings().StartingHealth);

        PlayerLevel.Instance.SetLevel(GameSettingsManager.Instance.GetGameSettings().StartingLevel);

        PvpMissionManager.Instance.GetNewMissions(GameSettingsManager.Instance.GetGameSettings().StartingAmountOfMissions);

    }


}
