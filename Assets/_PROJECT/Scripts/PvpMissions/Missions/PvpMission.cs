using UnityEngine;

public abstract class PvpMission :ScriptableObject
{

    public string Name;
    public string Description;
    public Sprite Icon;

    public PvpMissionRewards MissionRewards;

    public abstract void Evaluate();

    //Subscribe To Necessary Events
    public abstract void Load();

    //Unsubscribe Events
    public abstract void Unload();

}
