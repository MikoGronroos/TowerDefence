using UnityEngine;

public class VirtualCurrencyManager : MonoBehaviourSingleton<VirtualCurrencyManager>
{

    private VirtualCurrencyManagerUI _gameStateCurrencyManagerUI;

    [SerializeField] private int currentCurrency;
    [SerializeField] private int maxCurrency;

    [SerializeField] private int currentIncome;

    private void Awake()
    {
        _gameStateCurrencyManagerUI = GetComponent<VirtualCurrencyManagerUI>();
    }

    private void Start()
    {
        SetCurrency(500);
    }

    #region Currency

    public void RemoveCurrency(int value)
    {
        var currency = Mathf.Clamp(currentCurrency - value, 0, maxCurrency);
        SetCurrency(currency);
    }

    public void AddCurrency(int value)
    {
        var currency = Mathf.Clamp(currentCurrency + value, 0, maxCurrency);
        SetCurrency(currency);
    }

    public void SetCurrency(int value)
    {
        currentCurrency = value;
        _gameStateCurrencyManagerUI.UpdatePlayerCurrency(currentCurrency);
    }

    #endregion

    #region Income

    public void SetIncome(int value)
    {
        currentIncome = value;
    }

    #endregion

    public bool CheckIfPlayerHasEnoughCurrency(int value)
    {
        if (currentCurrency >= value)
        {
            return true;
        }
        return false;
    }

}
