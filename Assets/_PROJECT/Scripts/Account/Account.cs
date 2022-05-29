[System.Serializable]
public class Account
{

    public Account()
    {
        AccountLevel = 1;
        IsFirstLaunchOfTheGame = true;
    }

    public string AccountName;

    public int AccountLevel;
    public int AccountXp;

    public int CurrentTrophies;
    public int HighestTrophies;

    public int CurrentWinstreak;
    public int HighestWinstreak;

    public int TotalVictories;

    public int GamesPlayed;

    public bool AdsRemoved;
    public bool IsFirstLaunchOfTheGame;

}
