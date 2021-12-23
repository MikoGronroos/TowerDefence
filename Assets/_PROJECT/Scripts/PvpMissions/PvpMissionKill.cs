using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pvp Mission/Kill")]
public class PvpMissionKill : PvpMission
{

    public int CurrentAmount;
    public int AmountNeeded;
    public UnitStats UnitType;

    public bool IsReached
    {
        get
        {
            return CurrentAmount >= AmountNeeded;
        }
    }

    public override void Init()
    {
    }

}
