using System.Collections;
using UnityEngine;

public class LoanManager : MonoBehaviourSingleton<LoanManager>
{

    [SerializeField] private bool hasTakenLoan;
    [SerializeField] private bool hasPaidLoan;

    [SerializeField] private LoanSettings loanSettings;

    private int _currentLoan;

    public void GiveLoan()
    {
        if (!hasTakenLoan)
        {
            _currentLoan = CurrentLoan();
            VirtualCurrencyManager.Instance.AddCurrency(_currentLoan);
            hasTakenLoan = true;
            hasPaidLoan = false;

            foreach (var effect in loanSettings.LoanEffects)
            {
                EffectManager.Instance.AddEffect(effect);
            }

            StartCoroutine(LoanTimer());
        }
    }

    public int CurrentLoan()
    {
        return (int)(100 * (0.25 * PlayerLevel.Instance.GetCurrentLevel()));
    }

    public void PayLoan()
    {
        if (!hasTakenLoan) return;

        if (VirtualCurrencyManager.Instance.CheckIfPlayerHasEnoughCurrency(_currentLoan))
        {
            VirtualCurrencyManager.Instance.RemoveCurrency(_currentLoan);
            hasPaidLoan = true;
            hasTakenLoan = false;

            foreach (var effect in loanSettings.LoanEffects)
            {
                EffectManager.Instance.RemoveEffect(effect);
            }

        }
    }

    private IEnumerator LoanTimer()
    {

        yield return new WaitForSeconds(loanSettings.LoanPaymentTime);

        if (hasPaidLoan)
        {
        }
        else
        {
        }
        hasTakenLoan = false;
    }

}
