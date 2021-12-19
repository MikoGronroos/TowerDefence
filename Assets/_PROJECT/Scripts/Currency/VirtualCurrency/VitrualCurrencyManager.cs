using UnityEngine;

public class VitrualCurrencyManager : MonoBehaviourSingleton<VitrualCurrencyManager>
{

    private VitrualCurrencyManagerUI _gameStateCurrencyManagerUI;

    [SerializeField] private int currentCurrency;
    [SerializeField] private int maxCurrency;

    private void Awake()
    {
        _gameStateCurrencyManagerUI = GetComponent<VitrualCurrencyManagerUI>();
    }

    private void Start()
    {
        SetCurrency(500);
    }

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

    public bool CheckIfPlayerHasEnoughCurrency(int value)
    {
        if (currentCurrency >= value)
        {
            return true;
        }
        return false;
    }

}
