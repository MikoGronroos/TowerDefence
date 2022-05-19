[System.Serializable]
public class Account
{

    public Account(int level)
    {
        AccountLevel = level;
    }

    public string AccountName;

    public int AccountLevel;
    public int AccountXp;

    public int Winstreak;
    public int GamesPlayed;

    public bool AdsRemoved;

}
