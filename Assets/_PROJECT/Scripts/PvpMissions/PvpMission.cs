using UnityEngine;

public abstract class PvpMission :ScriptableObject
{

    public string Name;
    public string Description;

    public PvpMissionRewards MissionRewards;

    //Subscribe To Necessary Events
    public abstract void Init();

}
