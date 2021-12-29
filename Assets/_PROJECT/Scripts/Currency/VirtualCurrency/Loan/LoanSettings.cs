using UnityEngine;

[CreateAssetMenu(menuName = "Loan/Loan Settings")]
public class LoanSettings : ScriptableObject
{
    public float LoanPaymentTime;

    public Effect[] LoanEffects;

    public Effect[] LoanPaymentFailedEffects;

}
