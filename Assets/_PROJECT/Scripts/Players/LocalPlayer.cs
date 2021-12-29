using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalPlayer
{

    [SerializeField] private bool playerIsReady;

    [SerializeField] private int playerId;

    [SerializeField] private List<Turret> playerTurrets = new List<Turret>();

    public bool IsLocal;

    #region Turret

    public void AddTurret(Turret turret)
    {
        playerTurrets.Add(turret);
    }

    public IEnumerable<Turret> GetPlayerTurrets()
    {
        return playerTurrets;
    }

    #endregion

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
