using UnityEngine;

[CreateAssetMenu(menuName = "Account")]
public class Account : ScriptableObject
{

    public string AccountName;
    public string AccountId;

    public int AccountLevel;

    public int SoftCurrency;
    public int HardCurrency;

}
