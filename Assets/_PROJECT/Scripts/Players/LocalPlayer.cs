using UnityEngine;

[System.Serializable]
public class LocalPlayer
{

    [SerializeField] private bool playerIsReady;

    [SerializeField] private int playerId;

    [SerializeField] private int amountOfMapSkips = 1;

    public void TogglePlayerIsReady(bool value)
    {
        playerIsReady = value;
    }

    public bool IsPlayerReady()
    {
        return playerIsReady;
    }

    public int GetPlayerID()
    {
        return playerId;
    }

    public void SetPlayerID(int id)
    {
        playerId = id;
    }

    public int GetAmountOfMapSkips()
    {
        return amountOfMapSkips;
    }

    public void SetAmountOfMapSkips(int amount)
    {
        amountOfMapSkips = amount;
    }

}
