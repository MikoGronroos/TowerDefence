using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{

    [SerializeField] private GameSettings settings;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        EventCreator.CreateAllEvents();

        VirtualCurrencyManager.Instance.SetCurrency(settings.StartingCurrency);
        VirtualCurrencyManager.Instance.SetIncome(settings.StartingIncome);
        VirtualCurrencyManager.Instance.SetInterval(settings.IncomeInterval);

        HealthManager.Instance.SetHealhtOfEveryPlayer(settings.StartingHealth);

        PlayerLevel.Instance.SetLevel(settings.StartingLevel);

        PvpMissionManager.Instance.GetNewMissions(settings.StartingAmountOfMissions);

    }


}
