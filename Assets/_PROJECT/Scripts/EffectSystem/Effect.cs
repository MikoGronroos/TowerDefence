using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public float Addon;

    public Sprite IconName;

    public int Id;

    public abstract void Tick();

}