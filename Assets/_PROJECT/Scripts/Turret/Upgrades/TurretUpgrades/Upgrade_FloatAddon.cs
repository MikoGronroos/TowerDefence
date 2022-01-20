using UnityEngine;

public class Upgrade_FloatAddon : ScriptableObject
{

    public CustomFloat Value;

    public float Addon;

    public void Add()
    {
        Value.BaseValue += Addon;
    }

}
