using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoanManagerUI : MonoBehaviour
{

    [Header("Loan Menu")]

    [SerializeField] private Button loanMenuButton;
    [SerializeField] private GameObject loanMenuPanel;

    [Header("Loan")]

    [SerializeField] private Button loanButton;
    [SerializeField] private TextMeshProUGUI availableLoanAmountText;

    private void Awake()
    {

        loanButton.onClick.AddListener(() => {
            LoanManager.Instance.GiveLoan();
        });

        loanMenuButton.onClick.AddListener(() => {
            ToggleLoanMenu();
        });

    }

    private void ToggleLoanMenu()
    {
        availableLoanAmountText.text = $"{LoanManager.Instance.CurrentLoan()}$";
        loanMenuPanel.SetActive(!loanMenuPanel.activeSelf);
    }

}
