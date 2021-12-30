using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanManager : MonoBehaviourSingleton<LoanManager>
{

    [SerializeField] private bool hasTakenLoan;
    [SerializeField] private bool hasPaidLoan;

    [SerializeField] private LoanSettings loanSettings;

    private List<Effect> _givenEffects = new List<Effect>();

    private int _currentLoan;

    public void GiveLoan()
    {
        if (!hasTakenLoan)
        {
            _currentLoan = CurrentLoan();
            VirtualCurrencyManager.Instance.AddCurrency(_currentLoan);
            hasTakenLoan = true;
            hasPaidLoan = false;

            GiveEffects();

            StartCoroutine(LoanTimer());
        }
    }

    private void GiveEffects()
    {
        foreach (var turret in PlayerManager.Instance.GetLocalPlayer().GetPlayerTurrets())
        {
            var effect = new TurretEffect(0.75f, TurretEffectType.Damage);
            turret.AddEffect(effect);
            _givenEffects.Add(effect);
        }

        VirtualCurrencyManager.Instance.AddEffect(new CurrencyEffect(0.85f, CurrencyEffectType.Income));
    }
    private void RemoveEffects()
    {

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
        }
    }

    private IEnumerator LoanTimer()
    {

        yield return new WaitForSeconds(loanSettings.LoanPaymentTime);

        if (!hasPaidLoan)
        {
        }
        hasTakenLoan = false;
    }

}
