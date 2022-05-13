using UnityEngine;

public class AccountManager : MonoBehaviourSingletonDontDestroyOnLoad<AccountManager>
{

    [SerializeField] private Account currentAccount;

    public Account CurrentAccount
    {
        get
        {
            return currentAccount;
        }
        set
        {
            currentAccount = value;
        }
    }

    public void LoadData()
    {
        LoadAccountData.LoadData();
    }

    public void SaveData()
    {
        SaveAccountData.SaveTheAccountData(currentAccount);
    }

}
