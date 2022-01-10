using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalPlayer
{

    [SerializeField] private bool playerIsReady;

    [SerializeField] private int playerId;

    public bool IsLocal;

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

}
