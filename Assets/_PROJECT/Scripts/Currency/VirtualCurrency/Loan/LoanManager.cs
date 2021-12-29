using UnityEngine;

public class LoanManager : MonoBehaviourSingleton<LoanManager>
{

    [SerializeField] private bool hasTakenLoan;

    public void GiveLoan()
    {
        if (!hasTakenLoan)
        {
            VirtualCurrencyManager.Instance.AddCurrency(CurrentLoan());
            hasTakenLoan = true;
        }
    }

    public int CurrentLoan()
    {
        return (int)(100 * (0.25 * PlayerLevel.Instance.GetCurrentLevel()));
    }

}
